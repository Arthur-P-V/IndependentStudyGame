using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 1000f;
    public float inputSpeed = 50f;
    public float forwardSpeed = 10f;
    private Vector3 direction;
    public bool dead = false;
    public int health = 5;
    public float iFrameCount = 0.5f;
    public bool hit = false;
    public Animator animator;
    private Rigidbody rb;
    private bool isBouncing = false;
    
    public GameObject firePoint;
    public GameObject projectile;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        direction = new Vector3(Input.GetAxis("Horizontal") * inputSpeed, Input.GetAxis("Vertical") * inputSpeed, forwardSpeed);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!dead)
        {
            if (!isBouncing) rb.velocity = direction * speed * Time.fixedDeltaTime;
        }
        

    }

    private void OnCollisionEnter(Collision collision)
    {
        print("HIT");
        if (collision.collider.CompareTag("Wall") || collision.collider.CompareTag("Enemy")) {

            isBouncing = true;
            rb.AddForce(collision.contacts[0].normal * 15, ForceMode.Impulse); //Bounce Player Off collided Object
            Invoke("StopBounce", 0.2f);

            if (!hit) { //Keeps OnCollisionEnter from depleting health immediately by looping
                health--; 
                forwardSpeed -= forwardSpeed * 0.25f; // Decrease Speed by 25% on death
                

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
    }

    public void Shoot() {
        GameObject tempProjectile = Instantiate(projectile, firePoint.transform.position, Quaternion.identity);
        tempProjectile.GetComponent<Rigidbody>().AddForce(Vector3.forward * 5000, ForceMode.Force);
    }

    public void StopBounce() {
        isBouncing = false;
    }

    private void HitFunction() {
        StartCoroutine("HitTimer");
    }    

    private IEnumerator HitTimer() {
        hit = true;
        animator.SetBool("iFrames?", true);
        yield return new WaitForSeconds(1.0f);
        animator.SetBool("iFrames?", false);
        hit = false;
    }

}
