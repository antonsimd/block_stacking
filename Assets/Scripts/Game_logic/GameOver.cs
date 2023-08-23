using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public static void gameOver() {
        int newScore = Score.currentScore.getScore();

        // Update high score if needed
        if (newScore > PlayerPrefs.GetInt(MainMenu.highScoreKey)) {
            PlayerPrefs.SetInt(MainMenu.highScoreKey, newScore);
            PlayerPrefs.Save();
        }
    }
}
