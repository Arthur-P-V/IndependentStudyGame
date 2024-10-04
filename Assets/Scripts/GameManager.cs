using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject GameOver;
    public int score = 0;
    public PlayerController Player;
    public float scoreInterval = 0.05f;
    public TextMeshProUGUI scoreText;
    
    void Start()
    {
        StartCoroutine("TickScore");
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score;
        if (Player.dead) {
            GameOver.SetActive(true);
            if (Input.GetKeyDown(KeyCode.R)) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    public IEnumerator TickScore() {
        while (true)
        {
            score += 1;
            yield return new WaitForSeconds(scoreInterval);
        }
        
    }
}
