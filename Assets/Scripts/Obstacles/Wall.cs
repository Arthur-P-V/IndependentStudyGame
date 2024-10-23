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

    public override void Spawn(Vector3 spawnerTransform)
    {
        base.Spawn(spawnerTransform);

        int option = Random.Range(1, 4);

        switch (option) {
            case 1:
                gameObject.transform.position = new Vector3(spawnerTransform.x -30, spawnerTransform.y + 10, spawnerTransform.z);
                break;
            case 2:
                gameObject.transform.position = new Vector3(spawnerTransform.x + 30, spawnerTransform.y + 10, spawnerTransform.z);
                break;
            case 3:
                gameObject.transform.position = new Vector3(spawnerTransform.x + 9, spawnerTransform.y - 30, spawnerTransform.z);
                break;
            case 4:
                gameObject.transform.position = new Vector3(spawnerTransform.x + 9, spawnerTransform.y + 30, spawnerTransform.z);
                break;
        }
    }
}
