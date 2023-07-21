using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{

    public static Score instance;
    public TextMeshProUGUI scoreText;
    
    private int score = 0;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    private void Start() {
        setScore();
    }

    private void setScore() {
        scoreText.text = "Score: " + score.ToString();
    }

    public void addPoint() {
        score ++;
        setScore();
    }
}