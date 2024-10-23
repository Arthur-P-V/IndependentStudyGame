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
    public override void Spawn(Vector3 spawnerTransform) {
        base.Spawn(spawnerTransform);   

        float x = Random.Range(-20f, 20f);
        float y = Random.Range(-10f, 10f);
        float rotation = Random.Range(0f, 180f);
        Vector3 newTransform = new(x, y, spawnerTransform.z);
        gameObject.transform.SetPositionAndRotation(newTransform, Quaternion.Euler(0, 0, rotation));
    }
}
