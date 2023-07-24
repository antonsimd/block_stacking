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

    private GameObject box1Object;
    private GameObject box2Object;
    private GameObject box3Object;
    private GameObject box4Object;
    private Box box1;
    private Box box2;
    private Box box3;
    private Box box4;
    private GameObject ground;
    
    // draw the box before rendering the first frame, avoids lag
    private void Awake() {
        box1Object = Instantiate(boxPrefab, boxInitialPosition, Quaternion.identity);
        box1 = box1Object.GetComponent<Box>();
        ground = Instantiate(groundPrefab, groundPostition, Quaternion.identity);
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
            box2Object = Instantiate(boxPrefab, position, Quaternion.identity);
            box2 = box2Object.GetComponent<Box>();
        } else if (box3 == null) {
            box3Object = Instantiate(boxPrefab, position, Quaternion.identity);
            box3 = box3Object.GetComponent<Box>();
        } else if (box4 == null) {
            box1.moveBox(Vector3.down);
            box2.moveBox(Vector3.down);
            box3.moveBox(Vector3.down);
            ground.GetComponent<Ground>().moveGround(Vector3.down);

            box4Object = Instantiate(boxPrefab, position, Quaternion.identity);
            box4 = box4Object.GetComponent<Box>();
            Destroy(ground, 1);
        } 
        
        else {
            var tempObject = box1Object;
            var temp = box1;
            box1 = box2;
            box1Object = box2Object;
            box2 = box3;
            box2Object = box3Object;
            box3 = box4;
            box3Object = box4Object;

            temp.moveBox(Vector3.down);
            box1.moveBox(Vector3.down);
            box2.moveBox(Vector3.down);
            box3.moveBox(Vector3.down);
            box4Object = Instantiate(boxPrefab, position, Quaternion.identity);
            box4 = box4Object.GetComponent<Box>();
            Destroy(tempObject, 1);
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
            int offset = score == 0 ? (int)box1.getSize().y + INITIAL_BOX_Y_OFFSET : 2 * (int)box1.getSize().y + INITIAL_BOX_Y_OFFSET;
            newBox(offset);
        }
    }
}
