using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 100f;
    public float forwardSpeed = 10f;
    public bool dead = false;
    public int health = 5;
    public float iFrameCount = 0.5f;
    public bool hit = false;
    public bool iFrames = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            if (!hit)
            {
                transform.position = transform.position + new Vector3(0, 0, forwardSpeed) * Time.deltaTime;
            }
            else { 
                transform.position = transform.position + new Vector3(0,0, -(forwardSpeed / 4)) * Time.deltaTime;
            }


            float horizontalDirection = Input.GetAxis("Horizontal");
            float verticalDirection = Input.GetAxis("Vertical");

            transform.Translate(new Vector3(horizontalDirection * speed * Time.deltaTime, verticalDirection * speed * Time.deltaTime, 0));
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("HIT");
        if (collision.collider.CompareTag("Wall")) {
            forwardSpeed -= forwardSpeed * 0.25f; // Decrease Speed by 25% on death
            if (!hit) { //Keeps OnCollisionEnter from depleting health immediately by looping
                health--; 
            }

            if (health <= 0)
            {
                dead = true;
                return;
            }
            else
            {
                HitFunction();
                return;
            } 
        }
    }

    private void HitFunction() {
        StartCoroutine("hitTimer");
        StartCoroutine("iFrameTimer");
        StartCoroutine("iFrameRoutine");
    }    
    private IEnumerator iFrameRoutine() {
        MeshCollider collider = GetComponent<MeshCollider>();
        MeshRenderer visible = GetComponent<MeshRenderer>();


        collider.enabled = false;
        while (iFrames) {
            visible.enabled = false;
            yield return new WaitForSeconds(0.01f);
            visible.enabled = true;
        }
        collider.enabled = true;

    }
    private IEnumerator iFrameTimer()
    {
        iFrames = true;
        yield return new WaitForSeconds(1.25f);
        iFrames = false;
    }
    private IEnumerator hitTimer() {
        hit = true;
        yield return new WaitForSeconds(0.75f);
        hit = false;
    }

}
