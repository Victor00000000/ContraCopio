using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{

    int score = 0;
    public int lives;
    public TMPro.TMP_Text scoreText;
    public TMPro.TMP_Text livesText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score " + score;
        livesText.text = "Lives " + lives;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScorePoins(int points) {
        score += points;
        scoreText.text = "Score " + score;
    }

    public void UpdateLives(int amount) {
        lives += amount;
        livesText.text = "Lives " + lives;
    }
}
