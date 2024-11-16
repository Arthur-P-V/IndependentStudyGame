using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{

    public GameObject Player;
    public List<GameObject> ObstaclePrefabs;
    public List<GameObject> enemyPrefabs;
    public GameObject coin;
    public float spawnInterval = 3f;

    private List<GameObject> obstacleQueue = new List<GameObject>();
    private List<GameObject> obstaclesWaitingForQueue = new List<GameObject>();
    private List<GameObject> coinQueue = new List<GameObject>();
    private List<GameObject> enemyQueue = new List<GameObject>();
    private List<GameObject> enemiesWaitingForQueue = new List<GameObject>();
    
    private GameObject pool;

    //Tag objects with a level and spawn ALL OF THEM in the beginning? then as the player goes through the levels more objects are added to the pool for instantiation giving variety

    // Start is called before the first frame update
    private void Awake()
    {
        Player = GameObject.Find("Player");
    }
    void Start()
    {
        foreach (var obstacle in ObstaclePrefabs ) {
/*            var count = 0;
            if (obstacle.GetComponent<Obstacle>().obstacleLevel == 1) //This is literally to try and stave off lag and thats it
            {
                count = 10;
            }
            else {
                count = 4;
            }*/
            for (int i = 0; i < 10; i++)
            {
                GameObject temp = Instantiate(obstacle, transform.position, Quaternion.identity);

                if (temp.GetComponent<Obstacle>().obstacleLevel == 1) { 
                    obstacleQueue.Add(temp);
                }
                else
                {
                    obstaclesWaitingForQueue.Add(temp);
                }
                
            }
        }

        foreach (var enemy in enemyPrefabs) {
            for (int i = 0; i < 10; i++) {
                GameObject temp = Instantiate(enemy, transform.position, Quaternion.Euler(-90, 0, -180));

                if (temp.GetComponent<Enemy>().enemyLevel == 1)
                {
                    enemyQueue.Add(temp);
                }
                else { 
                    enemiesWaitingForQueue.Add(temp);
                }
            }
        }

        for (int i = 0; i < 20; i++) {
            GameObject tempCoin = Instantiate(coin, transform.position, Quaternion.identity );
            coinQueue.Add(tempCoin);
        } 

        obstacleQueue = obstacleQueue.OrderBy(x => Random.value).ToList(); // Shuffle List

        StartCoroutine("Runspawner");

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(0, 0, Player.transform.position.z + 150);

        if (GameManager.Instance.recentlyLeveled) { 
            switch (GameManager.Instance.varietyLevel) {
                case (GameManager.Variety.Low):
                    break;
                case (GameManager.Variety.Medium):
                    AddObstaclesOfLevel(GameManager.Instance.level);
                    break;
                case (GameManager.Variety.High):
                    AddObstaclesOfLevel(GameManager.Instance.level + 1);
                    break;
            }
            GameManager.Instance.recentlyLeveled= false; //setting this back to false ensures we don't run this again until the player levels again
        
        }
        
    }

    private IEnumerator Runspawner() {
        yield return new WaitForSeconds(spawnInterval);
        while (true) {
            if (!GameManager.Instance.GameOver) //Run the spawner till the game stops
            {
                Spawn();
                yield return new WaitForSeconds(spawnInterval / 3);
                SpawnCoin();
                yield return new WaitForSeconds(spawnInterval / 3);
                SpawnEnemy();
            }
            
            yield return new WaitForSeconds(spawnInterval / 3);  
        }   
    }

    private void SpawnCoin()
    {
        GameObject coinToSpawn = coinQueue.ElementAt(0);
        coinQueue.RemoveAt(0);
        coinQueue.Add(coinToSpawn);
        coinToSpawn.GetComponent<Coin>().Spawn(transform.position);
    }

    private void Spawn()
    {
        GameObject obstacleToSpawn = obstacleQueue.ElementAt(0);  //Mimics a FIFO queue but allows me to shuffle when changing levels and potentially adding new obstacles
        obstacleQueue.RemoveAt(0);
        obstacleQueue.Add(obstacleToSpawn);

        obstacleToSpawn.GetComponent<Obstacle>().Spawn(gameObject.transform.position);
    }

    private void SpawnEnemy() { 
        GameObject enemyToSpawn = enemyQueue.ElementAt(0);
        enemyQueue.RemoveAt(0);
        enemyQueue.Add(enemyToSpawn);


        enemyToSpawn.SetActive(true);
        enemyToSpawn.GetComponent<Enemy>().Spawn(gameObject.transform.position);
    }

    public void AddObstaclesOfLevel(int level) {
        for (int i = 0; i < obstaclesWaitingForQueue.Count; i++ ) {
            if (obstaclesWaitingForQueue.ElementAt(i).GetComponent<Obstacle>().obstacleLevel <= level) {
                GameObject temp = obstaclesWaitingForQueue.ElementAt(i);
                obstaclesWaitingForQueue.Remove(obstaclesWaitingForQueue.ElementAt(i));
                obstacleQueue.Insert(Random.Range(0, obstacleQueue.Count), temp);
            }
        }
    }
}
