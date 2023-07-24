using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{

    public static Score currentScore;
    public TextMeshProUGUI scoreText;
    
    private int score = 0;

    private void Awake() {
        currentScore = this;
    }

    // Start is called before the first frame update
    private void Start() {
        displayScore();
    }

    // update the score on the screen
    private void displayScore() {
        scoreText.text = "Score: " + score.ToString();
    }

    public void addPoint() {
        score ++;
        displayScore();
    }

    public int getScore() {
        return score;
    }
}