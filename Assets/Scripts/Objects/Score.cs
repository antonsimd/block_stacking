using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{

    public static Score currentScore;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    const string currentScoreString = "Score: ";
    const string highScoreString = "High score: ";
    
    int score = 0;
    int highScore;

    void Awake() {
        currentScore = this;
        highScore = PlayerPrefs.GetInt(MainMenu.highScoreKey);
    }

    // Start is called before the first frame update
    void Start() {
        displayScore();
        displayHighScore();
    }

    // update the score on the screen
    void displayScore() {
        scoreText.text = currentScoreString + score.ToString();
    }

    void displayHighScore() {
        if (score > highScore) {
            highScoreText.text = highScoreString + score.ToString();
        } else {
            highScoreText.text = highScoreString + highScore.ToString();
        }
    }

    public void addPoint() {
        score++;
        displayScore();
        displayHighScore();
    }

    public int getScore() {
        return score;
    }
}