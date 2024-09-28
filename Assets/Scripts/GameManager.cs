using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject GameOver;
    public int score = 0;
    public PlayerController Player;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.dead) {
            GameOver.SetActive(true);
            if (Input.GetKeyDown(KeyCode.R)) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
