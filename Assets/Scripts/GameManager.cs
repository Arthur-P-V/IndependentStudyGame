using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UIElements;
using System.Linq;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static GameManager Instance;
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
    public List<TextMeshProUGUI> Scores = new List<TextMeshProUGUI>();

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        //PlayerPrefs.DeleteAll();
        bestScore = PlayerPrefs.GetInt("0", 0); //Grabs the highest score (index 0) or returns 0
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
            Scores[i].text = $"{i+1}. {PlayerPrefs.GetInt($"{i}", 0)}";
        }
    
    }

    public void UpdateLeaderboard(int score)
    {
        int scoreIndex = 0; //Index of position for new entry
        int tempScore = 0; //container for scores when shifting entries
        for (int i = 0; i < 5; i++)
        {
            if (score > PlayerPrefs.GetInt($"{i}", 0))
            { // Grab Index of position for new score
                scoreIndex = i;
                break;
            }
        }

        for (int i = scoreIndex; i < 5; i++) {
            if (PlayerPrefs.HasKey($"{i}"))
            {
                tempScore =  PlayerPrefs.GetInt($"{i}", 0);
                PlayerPrefs.DeleteKey($"{i}");
            }
            
            PlayerPrefs.SetInt($"{i}", score);
            score = tempScore;
        }

        PlayerPrefs.Save();

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
