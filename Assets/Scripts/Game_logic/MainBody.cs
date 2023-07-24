using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBody : MonoBehaviour
{
    // CONSTANTS
    private const int INITIAL_BOX_Y_OFFSET = -4;
    private const int GROUND_Y_OFFSET = -5;
    
    // PUBLIC - prefabs
    public GameObject boxPrefab;
    public GameObject groundPrefab;

    // box position offsets
    private Vector2 boxInitialPosition = new Vector2(0, INITIAL_BOX_Y_OFFSET);
    private Vector2 groundPostition = new Vector2(0, GROUND_Y_OFFSET);
    // instances of game objects
    private Box box1;
    private Box box2;
    private GameObject ground;
    
    // draw the box before rendering the first frame, avoids lag
    private void Awake() {
        box1 = Instantiate(boxPrefab, boxInitialPosition, Quaternion.identity).GetComponent<Box>();
        ground = Instantiate(groundPrefab, groundPostition, Quaternion.identity);
    }

    // Update is called once per frame
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (box2 != null) {
                box2.stopMovement();
            } else {
                box1.stopMovement();
            }
            int score = Score.currentScore.getScore();
            if (score == 0) {
                Score.currentScore.addPoint();
                int offset = (int)box1.getSize().y + INITIAL_BOX_Y_OFFSET;
                newBox(offset);
            } else {
                if (!CheckLoss.checkLoss(box2, box1)) {
                    Score.currentScore.addPoint();
                    int offset = (score + 1) * (int)box1.getSize().y + INITIAL_BOX_Y_OFFSET;
                    newBox(offset);
                }
            }
        }
    }

    private void newBox(int y) {
        Vector2 position = new Vector2(0, y);
        box2 = Instantiate(boxPrefab, position, Quaternion.identity).GetComponent<Box>();
    }
}
