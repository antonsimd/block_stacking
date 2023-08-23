using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public static string highScoreKey = "highScore";
    public TextMeshProUGUI highScore;

    public void playGame() {
        SceneManager.LoadScene("Game");
    }

    void Awake() {
        // Check for an existing high score, otherwise set to 0
        if (!PlayerPrefs.HasKey(highScoreKey)) {
            PlayerPrefs.SetInt(highScoreKey, 0);
            PlayerPrefs.Save();
        }
    }

    void Start() {
        displayHighScore();
    }

    void displayHighScore() {
        int score = PlayerPrefs.GetInt(highScoreKey);
        highScore.text = "High score: " + score.ToString();
    }
}
