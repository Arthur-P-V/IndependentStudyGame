using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Obstacle
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

    public override void Spawn(Transform spawnerTransform)
    {
        base.Spawn(spawnerTransform);

        int option = Random.Range(1, 4);

        switch (option) {
            case 1:
                transform.position = new Vector3(spawnerTransform.position.x -30, spawnerTransform.position.y + 10, spawnerTransform.position.z);
                break;
            case 2:
                transform.position = new Vector3(spawnerTransform.position.x + 30, spawnerTransform.position.y + 10, spawnerTransform.position.z);
                break;
            case 3:
                transform.position = new Vector3(spawnerTransform.position.x + 9, spawnerTransform.position.y - 30, spawnerTransform.position.z);
                break;
            case 4:
                transform.position = new Vector3(spawnerTransform.position.x + 9, spawnerTransform.position.y + 30, spawnerTransform.position.z);
                break;
        }
    }
}
