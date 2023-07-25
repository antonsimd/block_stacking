using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBody : MonoBehaviour
{
    // CONSTANTS
    private const int INITIAL_BOX_Y_OFFSET = -4;
    private const int GROUND_Y_OFFSET = -5;
    private const int BOXES_MOVE_DOWN = 2;
    private const float BOX_SPREAD = 0.01f;
    
    // PUBLIC - prefabs
    public GameObject boxPrefab;
    public GameObject groundPrefab;

    // box position offsets
    private Vector2 boxInitialPosition = new Vector2(0, INITIAL_BOX_Y_OFFSET);
    private Vector2 groundPostition = new Vector2(0, GROUND_Y_OFFSET);
    private Vector3 initialBoxScale = new Vector3(2.5f, 1f, 1f);
    // instances of game objects

    private Box box1;
    private Box box2;
    private Box box3;
    private Box box4;
    private Ground ground;
    
    // draw the box before rendering the first frame, avoids lag
    private void Awake() {
        box1 = Box.createBox(boxPrefab, boxInitialPosition, initialBoxScale);
        ground = Ground.createGround(groundPrefab, groundPostition);
    }

    // Update is called once per frame
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            spaceKeyPressed();
        }
    }

    private void newBox(float y) {
        Vector2 position = new Vector2(0, y);

        if (box2 == null) {
            box2 = Box.createBox(boxPrefab, position, box1.getSize());
        } else if (box3 == null) {
            box3 = Box.createBox(boxPrefab, position, box2.getSize());
        } else if (box4 == null) {
            box1.moveBox(Vector3.down);
            box2.moveBox(Vector3.down);
            box3.moveBox(Vector3.down);
            ground.moveGround(Vector3.down);

            box4 = Box.createBox(boxPrefab, position, box3.getSize());
        } else {
            box1.moveBox(Vector3.down);
            box1 = box2;
            box2 = box3;
            box3 = box4;

            box1.moveBox(Vector3.down);
            box2.moveBox(Vector3.down);
            box3.moveBox(Vector3.down);
            box4 = Box.createBox(boxPrefab, position, box4.getSize());
        }
    }

    private void spaceKeyPressed() {
        if (box4 != null) {
            box4.stopMovement();
        } else if (box3 != null) {
            box3.stopMovement();
        } else if (box2 != null) {
            box2.stopMovement();
        } else {
            box1.stopMovement();
        }

        if (box2 == null) {
            if(!CheckLoss.checkLoss(box2, box1)) {
                continueGame(box1, null);
            }
        } else if (box3 == null) {
            if(!CheckLoss.checkLoss(box2, box1)) {
                continueGame(box2, box1);
            }
        } else if (box4 == null) {
            if(!CheckLoss.checkLoss(box3, box2)) {
                continueGame(box3, box2);
            }
        } else {
            if (!CheckLoss.checkLoss(box4, box3)) {
                continueGame(box4, box3);
            }
        }
    }

    private void continueGame(Box boxUpper, Box boxLower) {
        int score = Score.currentScore.getScore();
        Score.currentScore.addPoint();

        if (boxUpper != null && boxLower != null) {
            boxUpper.cutBox(boxLower);
        }

        float offset = score < BOXES_MOVE_DOWN ? 
            (float)boxUpper.getCentre().y + (float)boxUpper.getSize().y : (float)boxUpper.getCentre().y;

        offset += BOX_SPREAD;

        newBox(offset);
    }
}
