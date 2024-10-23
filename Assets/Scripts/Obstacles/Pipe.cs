using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : Obstacle
{
    protected override void Awake()
    {
        //base.Awake();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Spawn(Transform spawnerTransform) {
        base.Spawn(spawnerTransform);   
        
        print(spawnerTransform.position);

        print("PipeSpawner");

        float x = Random.Range(-20f, 20f);
        float y = Random.Range(-10f, 10f);
        float rotation = Random.Range(0f, 180f);

        transform.SetPositionAndRotation(new Vector3(x, y, spawnerTransform.position.z), Quaternion.Euler(0, 0, rotation));
    }
}
