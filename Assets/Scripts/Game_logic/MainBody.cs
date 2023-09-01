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
        Ground.createGround();
    }

    void Update() {
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began) {
                buttonPressed();
            }
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
