using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UIElements;
using System.Linq;

public class GameManager : Singleton<GameManager>
{
    // Start is called before the first frame update
    public GameObject GameOverText;
    public bool GameOver = false;
    public bool saved = false;
    public int score = 0;
    public int bestScore;
    public PlayerController Player;
    public float scoreInterval = 0.05f;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    public GameObject Leaderboard;
    public List<int> lbScores = new List<int>();
    public List<TextMeshProUGUI> lbEntries = new List<TextMeshProUGUI>();

    void Start()
    {
        score = 0;
        //PlayerPrefs.DeleteAll();
        bestScore = lbScores.First(); //Grabs the highest score (index 0) or returns 0
        highScoreText.text = "Best: " + bestScore;
        StartCoroutine("TickScore");
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score;

        if (score > bestScore) {
            highScoreText.text = "Best: " + score;
        }



        if (Player.dead) {
            GameOverText.SetActive(true);
            GameOver = true;
            if (Input.GetKeyDown(KeyCode.R)) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        if (GameOver && !saved)
        {
            UpdateLeaderboard(score);
                //else
                //{
                //    Scores[i].text = $"{i}. {PlayerPrefs.GetInt($"{i}", 0)}";
                //}
            Leaderboard.SetActive(true);
            saved = true;
            DisplayLeaderboard();
        }

    }


    public void DisplayLeaderboard() {
        for (int i = 0; i < 5; i++)
        {
            lbEntries[i].text = $"{i+1}. {PlayerPrefs.GetInt($"{i}", 0)}";
        }
    
    }

    public void UpdateLeaderboard(int score)
    {
        lbScores.Add(score);
        lbScores.Sort();
    }

    public IEnumerator TickScore() {
        while (true)
        {
            if (!Player.dead)
            {
                score += 1;
            }
            yield return new WaitForSeconds(scoreInterval);
        }
        
    }
}
