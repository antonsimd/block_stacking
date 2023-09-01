using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI currentScore;
    public TextMeshProUGUI highScore;
    public static GameOver instance;

    const string currentScoreText = "Well done!\nScore: ";
    const string highScoreText = "High Score: ";
    const string congratulationsScoreText = "Congratulations!\nYour new high score: ";

    int score;

    void Awake() {
        instance = this;
    }

    void Start() {
        gameObject.SetActive(false);
    }

    public void gameOver() {
        gameObject.SetActive(true);
        score = Score.currentScore.getScore();
        if (updateHighScore()) {          
            displayCongratulations();
        } else {
            displayCurrentScore();
        }

        displayHighScore();
    }

    bool updateHighScore() {
        if (score > PlayerPrefs.GetInt(MainMenu.highScoreKey)) {
            PlayerPrefs.SetInt(MainMenu.highScoreKey, score);
            PlayerPrefs.Save();
            return true;
        } else {
            return false;
        }
    }
    
    public void returnToMenu() {
        Cloud.clearClouds();
        Box.clearBoxes();
        SceneManager.LoadScene("MainMenu");
    }

    void displayCurrentScore() {
        currentScore.text = currentScoreText + score.ToString();
    }

    void displayHighScore() {
        highScore.text = highScoreText + PlayerPrefs.GetInt(MainMenu.highScoreKey).ToString();
    }

    void displayCongratulations() {
        currentScore.text = congratulationsScoreText + score.ToString();
    }
}
