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
    public PlayerController Player;

    //Behavior Control Variables
    public bool GameOver = false;
    public bool saved = false;
    public List<int> lbScores = new List<int>();
    public Feedback feedbackLevel = Feedback.Medium;
    public Variety varietyLevel = Variety.Medium;
    public bool recentlyLeveled = false;
    
    //Stat and Behavior at the same time
    public int level = 1;
    public int scoreToNextLevel = 100;
    
    //Stat Variables
    public int score = 0;
    public int bestScore;
    public int coins = 0;

    public float scoreInterval = 0.05f;

    //Canvas UI Objects
    public GameObject Leaderboard;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI SpeedText;
    public TextMeshProUGUI runTimeText;
    public List<TextMeshProUGUI> lbEntries = new List<TextMeshProUGUI>();
    public GameObject GameOverText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI healthText;
    public GameObject mediumFeedback;
    public GameObject highFeedback;

    public enum Feedback { 
        Low,
        Medium,
        High
    }

    public enum Variety { 
        Low,
        Medium,
        High
    }

    void Start()
    {
        score = 0;
        //PlayerPrefs.DeleteAll();
        bestScore = lbScores.FirstOrDefault(); //Grabs the highest score (index 0) or returns 0
        EnableFeedback();
        StartCoroutine("TickScore");

        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score;

        levelText.text = "Level: " + level;
        coinText.text = "Coins: " + coins;
        healthText.text = "Health: " + Player.health;
        distanceText.text = "" + (int)Player.transform.position.z + " feet";
        SpeedText.text = "" + (int)Player.forwardSpeed + "\nSpeed";

        if (score > bestScore) {
            highScoreText.text = "Best: " + score;
        }
        else
        {
            highScoreText.text = "Best: " + bestScore;
        }

        if (score > scoreToNextLevel) {
            level += 1;
            CalculateNextScore();
            if(Player.forwardSpeed < 200)
            {
                Player.forwardSpeed += Player.forwardSpeed * 0.1f; //Speed up by 10% every level
            }
           
            recentlyLeveled = true; //Signal the spawner to check if more obstacles should be queued
        }

        if (Player.dead) {
            GameOverText.SetActive(true);
            GameOver = true;
            if (Input.GetKeyDown(KeyCode.R)) {
                Reset();
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
            saved = true;
            if (feedbackLevel == Feedback.Medium || feedbackLevel == Feedback.High)
            {
                Leaderboard.SetActive(true);
                DisplayLeaderboard();
            }
        }

    }


    public void DisplayLeaderboard() {
        for (int i = 0; i < 5; i++)
        {
            lbEntries[i].text = $"{i+1}. {lbScores.ElementAtOrDefault(i)}";
        }
    
    }

    public void Reset()
    { 
        score = 0;
        level = 1;
        scoreToNextLevel = 100;
        bestScore = lbScores.FirstOrDefault();
        Leaderboard.SetActive(false);
        GameOverText.SetActive(false);
        GameOver = false;
        saved = false;
        Player.dead = false;
        Player.transform.position = new Vector3(0, 0, -20);
        Player.health = 5;
        StartCoroutine("TickScore");
        
    }

    public void UpdateLeaderboard(int score)
    {
        lbScores.Add(score);
        lbScores.Sort((a, b) => b.CompareTo(a)); //Sort Descending
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

    public void CalculateNextScore() {
        if (level <= 5) {
            scoreToNextLevel = (level + 1) * 100;
        }
        else
        {
            scoreToNextLevel = (int)(15 * Mathf.Pow(level + 1, 2));
        }
    }

    public void EnableFeedback() {
        if (feedbackLevel == Feedback.Medium) {
            mediumFeedback.SetActive(true);
        }
        if (feedbackLevel == Feedback.High)
        {
            mediumFeedback.SetActive(true);
            highFeedback.SetActive(true);
        }
    }
}
