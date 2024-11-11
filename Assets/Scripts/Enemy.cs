using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 1;
    public int enemyLevel = 1;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage() {
        health--;
        if (health <= 0) {
            Die();
        }
    }

    public void Die() {
        gameObject.SetActive(false);
    }

    public void Spawn(Vector3 spawnerTransform) {
        //base.Spawn(spawnerTransform);

        float x = Random.Range(-20f, 20f);
        float y = Random.Range(-10f, 10f);
        float rotation = Random.Range(0f, 180f);
        Vector3 newTransform = new(x, y, spawnerTransform.z);
        gameObject.transform.SetPositionAndRotation(newTransform, Quaternion.Euler(0, 0, rotation));
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player")) {
            TakeDamage();
        }
    }

}
