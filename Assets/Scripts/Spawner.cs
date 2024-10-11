using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.Subsystems;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{

    public GameObject Player;
    public GameObject Bar;
    public float spawnInterval = 1f;

    private List<GameObject> obstacleQueue = new List<GameObject>();
    
    private GameObject pool;

    //Tag objects with a level and spawn ALL OF THEM in the beginning? then as the player goes through the levels more objects are added to the pool for instantiation giving variety

    // Start is called before the first frame update
    void Start()
    {
        pool = transform.GetChild(0).gameObject;

        for (int i = 0; i < 10; i++) {
            GameObject tempbar = Instantiate(Bar, pool.transform.position, Quaternion.identity);
            obstacleQueue.Add(tempbar);
        }

        //foreach (GameObject tempbar in obstacleQueue)
        //{
        //   print(tempbar);
       // }

        StartCoroutine("Runspawner");

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(0, 0, Player.transform.position.z + 150);


    }

    private IEnumerator Runspawner() {

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
        //print(obstacleToSpawn.name);
        obstacleQueue.RemoveAt(0);
        obstacleQueue.Add(obstacleToSpawn);

        float x = Random.Range(transform.position.x - 20f, transform.position.x + 20f);
        float y = Random.Range(transform.position.y - 10f, transform.position.y + 10f);
        float rotation = Random.Range(0f, 180f);

        obstacleToSpawn.transform.position = new Vector3(x, y, transform.position.z);
        obstacleToSpawn.transform.rotation = Quaternion.Euler(0, 0, rotation);
    }
}
