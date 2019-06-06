using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{

    int score = 0;
    public int lives = 3;
    public TMPro.TMP_Text scoreText;
    public TMPro.TMP_Text livesText;
    public TMPro.TMP_InputField highScoreInput;
    public TMPro.TMP_Text highScoreListText;
    public GameObject gameOverPanel;
    public GameObject newHighscorePanel;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score " + score;
        livesText.text = "Lives " + lives;
    }

    public void ScorePoins(int points) {
        score += points;
        scoreText.text = "Score " + score;
    }

    public void UpdateLives(int amount) {
        lives += amount;
        livesText.text = "Lives " + lives;
    }

    public void GameOver() {
        int highScore = PlayerPrefs.GetInt("highscore");

        if (score > highScore) {
            newHighscorePanel.SetActive(true);
        } else {
            gameOverPanel.SetActive(true);
            highScoreListText.text = "HIGH SCORE\n\n" + PlayerPrefs.GetString("highscoreName") + " " + PlayerPrefs.GetInt("highscore");
        }
    }

    public void HighScoreInput() {
        string newInput = highScoreInput.text;
        Debug.Log(newInput);
        newHighscorePanel.SetActive(false);
        gameOverPanel.SetActive(true);
        PlayerPrefs.SetString("highscoreName", newInput);
        PlayerPrefs.SetInt("highscore", score);

        highScoreListText.text = "HIGH SCORE\n\n" + PlayerPrefs.GetString("highscoreName") + " " + PlayerPrefs.GetInt("highscore");
    }

    public void PlayAgain() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("again");
    }

    public void GoToMainMenu() {
        SceneManager.LoadScene("StartMenu");
    }
}
