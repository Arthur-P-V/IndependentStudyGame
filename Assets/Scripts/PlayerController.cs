using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 100f;
    public float forwardSpeed = 10f;
    public bool dead = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            transform.position = transform.position + new Vector3(0, 0, forwardSpeed) * Time.deltaTime;

            float horizontalDirection = Input.GetAxis("Horizontal");
            float verticalDirection = Input.GetAxis("Vertical");

            transform.Translate(new Vector3(horizontalDirection * speed * Time.deltaTime, verticalDirection * speed * Time.deltaTime, 0));
        }
        
        
    }

    private void OnCollisionEnter(Collision collision)
    {

        print("HIT");
        if (collision.collider.CompareTag("Wall")) {
            dead = true;

        }
    }
}
