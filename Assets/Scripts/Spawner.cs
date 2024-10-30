using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{

    public GameObject Player;
    public List<GameObject> ObstaclePrefabs;
    public float spawnInterval = 1f;

    private List<GameObject> obstacleQueue = new List<GameObject>();
    private List<GameObject> waitingForQueue = new List<GameObject>();
    
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
            for (int i = 0; i < 10; i++)
            {
                GameObject temp = Instantiate(obstacle, transform.position, Quaternion.identity);

                if (temp.GetComponent<Obstacle>().obstacleLevel == 1) { 
                    obstacleQueue.Add(temp);
                }
                else
                {
                    waitingForQueue.Add(temp);
                }
                
            }
            
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
            }
            
            yield return new WaitForSeconds(spawnInterval);  
        }


        
    }

    private void Spawn()
    {
        GameObject obstacleToSpawn = obstacleQueue.ElementAt(0);  //Mimics a FIFO queue but allows me to shuffle when changing levels and potentially adding new obstacles
        obstacleQueue.RemoveAt(0);
        obstacleQueue.Add(obstacleToSpawn);

        obstacleToSpawn.GetComponent<Obstacle>().Spawn(gameObject.transform.position);
    }

    public void AddObstaclesOfLevel(int level) {
        for (int i = 0; i < waitingForQueue.Count; i++ ) {
            if (waitingForQueue.ElementAt(i).GetComponent<Obstacle>().obstacleLevel <= level) {
                GameObject temp = waitingForQueue.ElementAt(i);
                waitingForQueue.Remove(waitingForQueue.ElementAt(i));
                obstacleQueue.Insert(Random.Range(0, obstacleQueue.Count), temp);
            }
        }
    }
}
