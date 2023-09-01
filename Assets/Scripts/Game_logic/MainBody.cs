using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBody : MonoBehaviour
{
    // PUBLIC - prefabs
    public GameObject boxPrefab;
    public GameObject groundPrefab;
    public GameObject boxCutawayPrefab;
    public GameObject gameOverPrefab;
    public GameObject cloudPrefab;
    public static MainBody mainBody;

    void Awake() {
        mainBody = this;
    }

    void Start() {
        Box.drawInitialBox();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            buttonPressed();
        }
    }

    void buttonPressed() {
        if (Box.checkLoss()) {
            Box.stopBoxes();
            GameOver.instance.gameOver();
        } else {
            Score.currentScore.addPoint();
            Box.moveUp();
        }
    }
}
