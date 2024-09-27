using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shifter : MonoBehaviour
{
    // Start is called before the first frame update

    public int TubeCount = 3;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        print("HIT");

        if (other.CompareTag("Player"))
        {
            print("BIGHIT");
            transform.position = transform.position + new Vector3(0, 0, TubeCount * 50);
        }
    }
}
