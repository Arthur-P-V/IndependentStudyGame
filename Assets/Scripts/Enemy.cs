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
        GameManager.Instance.killCount++;
    }

    public void Spawn(Vector3 spawnerTransform) {
        //base.Spawn(spawnerTransform);

        float x = Random.Range(-20f, 20f);
        float y = Random.Range(-10f, 10f);
        Vector3 newTransform = new(x, y, spawnerTransform.z);
        gameObject.transform.position = newTransform;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Projectile")) {
            TakeDamage();
        }
    }

}
