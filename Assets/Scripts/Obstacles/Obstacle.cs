using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int obstacleLevel = 1;

    protected virtual void Awake()
    {
        //spawner = GameObject.Find("Spawner");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Spawn(Transform spawnerTransform) { 
        
    }
}
