using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            GameManager.Instance.score += 50;
            GameManager.Instance.coins += 1;
            gameObject.SetActive(false);
        }
    }

    public void Spawn(Vector3 sTransform) {
        gameObject.SetActive(true);

        float x = Random.Range(-20f, 20f);
        float y = Random.Range(-6f, 6f);

        transform.position = new Vector3(sTransform.x + x, sTransform.y + y, sTransform.z);
        transform.rotation = Quaternion.Euler(new Vector3(-90, 0, 0));

    }
}
