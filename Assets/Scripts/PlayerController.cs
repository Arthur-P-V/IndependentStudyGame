using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 100f;
    public float forwardSpeed = 10f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + new Vector3(0, 0, forwardSpeed) * Time.deltaTime;

        float horizontalDirection = Input.GetAxis("Horizontal");
        float verticalDirection = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(horizontalDirection * speed * Time.deltaTime, verticalDirection * speed * Time.deltaTime, 0));
        
    }
}
