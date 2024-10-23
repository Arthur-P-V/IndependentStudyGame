using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.Subsystems;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{

    public GameObject Player;
    public List<GameObject> ObstaclePrefabs;
    public float spawnInterval = 1f;

    private List<GameObject> obstacleQueue = new List<GameObject>();
    
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
                obstacleQueue.Add(temp);
            }
            
        }
        obstacleQueue = obstacleQueue.OrderBy(x => Random.value).ToList(); // Shuffle List

        StartCoroutine("Runspawner");

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(0, 0, Player.transform.position.z + 150);

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
}
