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
    private Box box3;
    private Box box4;
    private Ground ground;
    
    // draw the box before rendering the first frame, avoids lag
    private void Awake() {
        box1 = Box.createBox(boxPrefab, boxInitialPosition);
        ground = Ground.createGround(groundPrefab, groundPostition);
    }

    // Update is called once per frame
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            spaceKeyPressed();
        }
    }

    private void newBox(int y) {
        Vector2 position = new Vector2(0, y);

        if (box2 == null) {
            box2 = Box.createBox(boxPrefab, position);
        } else if (box3 == null) {
            box3 = Box.createBox(boxPrefab, position);
        } else if (box4 == null) {
            box1.moveBox(Vector3.down);
            box2.moveBox(Vector3.down);
            box3.moveBox(Vector3.down);
            ground.moveGround(Vector3.down);

            box4 = Box.createBox(boxPrefab, position);
        } 
        
        else {
            box1.moveBox(Vector3.down);
            box1 = box2;
            box2 = box3;
            box3 = box4;

            box1.moveBox(Vector3.down);
            box2.moveBox(Vector3.down);
            box3.moveBox(Vector3.down);
            box4 = Box.createBox(boxPrefab, position);
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

        int score = Score.currentScore.getScore();

        if(!CheckLoss.checkLoss(box3, box2)) {
            Score.currentScore.addPoint();
            int offset = score == 0 ? 
                (int)box1.getSize().y + INITIAL_BOX_Y_OFFSET : 2 * (int)box1.getSize().y + INITIAL_BOX_Y_OFFSET;
            newBox(offset);
        }
    }
}
