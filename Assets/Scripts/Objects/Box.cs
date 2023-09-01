using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Box : MonoBehaviour
{
    // FIX IF NEEDED
    static Vector4 cameraDimensions = new Vector4(-2.5f, 2.5f, -5f, 5f);

    // Constants
    const int SPEED = 300;
    const int INITIAL_BOX_Y_OFFSET = -3;
    const float CUTAWAY_OFFSET = 0.5f;
    const int BOXES_MOVE_DOWN = 4;
    const float ySpeed = 10f;
    const int LEFT = -1;
    const int RIGHT = 1;

    // List for storing all boxes on the screen
    static List<Box> boxes = new List<Box>();
    static int boxCount = 0;
    static Vector2 boxInitialPosition = new Vector2(0, INITIAL_BOX_Y_OFFSET);
    static Vector3 boxInitialScale = new Vector3(2.5f, 1f, 1f);
    static System.Random random = new System.Random();

    // Game object and game related
    int direction;
    bool moveRequired = false;
    bool initialBox = false;
    Vector3 targetPosition;
    Vector2 movementVectorH = new Vector2(SPEED, 0);
    Rigidbody2D rigidbodyComponent;

    // Draw the first and second boxes on the screen, initialise game
    // Adds the instantiated objects to boxes list
    public static void drawInitialBox() {
        var newObject = Instantiate(MainBody.mainBody.boxPrefab, boxInitialPosition, Quaternion.identity);
        var script = newObject.GetComponent<Box>();

        newObject.transform.localScale = boxInitialScale;

        // Set initial box to true so it does not move
        script.initialBox = true;
        boxes.Add(script);
        boxCount++;

        var positionY = boxes[boxCount - 1].getCentre().y + boxes[boxCount - 1].getSize().y;
        var newPositionVector = new Vector3(0, positionY, 0);

        createBox(newPositionVector, boxInitialScale);
    }

    public static bool checkLoss() {
        return CheckLoss.checkLoss(boxes[boxCount - 1], boxes[boxCount - 2]);
    }

    public static void clearBoxes() {
        boxCount = 0;
        boxes.Clear();
    }
    // Stop all boxes
    public static void stopBoxes() {
        foreach (Box box in boxes) {
            box.stopMovement();
        }
    }
    // Create a box cutaway and a new box after the space button has been pressed
    public static void moveUp() {
        int numElements = boxes.Count;
        var lowerBox = boxes[boxCount - 1];
        lowerBox.cutBox(boxes[boxCount - 2]);

        // Stop movement of every single box
        foreach (Box box in boxes) {
            box.stopMovement();
        }

        if (boxCount < BOXES_MOVE_DOWN) {

            // Create a box above the previous box
            var positionY = lowerBox.getCentre().y + lowerBox.getSize().y;
            var newPositionVector = new Vector3(0, positionY, 0);

            createBox(newPositionVector, lowerBox.getSize());
        } else {

            // Move every box down and spawn the box at the same height as the previous box
            foreach (Box box in boxes) {
                box.moveDown();
            }

            var newPositionVector = new Vector3(0, lowerBox.getCentre().y, 0);
            createBox(newPositionVector, lowerBox.getSize());
        }
    }

    // Instantiate a box at a given position with a given scale
    // Adds the instantiated object to boxes list
    static void createBox(Vector3 position, Vector3 scale) {
        var newObject = Instantiate(MainBody.mainBody.boxPrefab, position, Quaternion.identity);
        var script = newObject.GetComponent<Box>();

        newObject.transform.localScale = scale;

        boxes.Add(script);
        boxCount++;
    }

    void Start() {
        rigidbodyComponent = GetComponent<Rigidbody2D>();

        // Random direction (left of right);
        direction = random.Next(0, 2) == 0 ? LEFT : RIGHT;

        // First box does not move
        direction = initialBox == true ? 0 : direction;
    }

    void Update() {
        if (getCentre().y < cameraDimensions.z) {
            destroyBox();
        }

        // If a move is required and the box has not been moved down yet, move the box down
        if (moveRequired && transform.position != targetPosition) {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, ySpeed * Time.deltaTime);
        } else {
            moveRequired = false;
        }

        // Horizontal velocity of the box
        rigidbodyComponent.velocity = movementVectorH * direction * Time.deltaTime;
    }

    void stopMovement() {
        direction = 0;
    }

    // Set the target coordinates of a box move to a position boxSizeY below
    void moveDown() {
        var initialPosition = getCentre();
        targetPosition = initialPosition - new Vector3(0, getSize().y, 0);
        moveRequired = true;
    }

    void destroyBox() {
        // Remove instance from boxes list
        boxes.Remove(this);
        boxCount--;
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        // Reverse the direction on collision with camera
        if (collision.tag == "MainCamera") {
            direction *= -1;
        }
    }

    public Vector3 getCentre() {
        return transform.position;
    }

    public Vector3 getSize() {
        return transform.localScale;
    }

    void cutBox(Box boxLower) {
        // get size of boxes in Vector2(size_x, size_y)
        Vector3 boxLowerSize = boxLower.getSize();
        Vector3 boxUpperSize = getSize();
        
        // get centre and world positions in Vector3
        Vector3 boxLowerPosition = boxLower.getCentre();
        Vector3 boxUpperPosition = getCentre();

        // calculate left and right boundaries of both boxes based on centre and size
        float leftLow = boxLowerPosition.x - (boxLowerSize.x / 2f);
        float leftUpp = boxUpperPosition.x - (boxUpperSize.x / 2f);
        float rightLow = boxLowerPosition.x + (boxLowerSize.x / 2f);
        float rightUpp = boxUpperPosition.x + (boxUpperSize.x / 2f);

        // if upper box is further to the left than lower box
        if (leftLow > leftUpp) {
            var difference = leftLow - leftUpp;

            // shift upper box right and decrease the horizontal scale
            transform.position += new Vector3(difference / 2, 0f, 0f);
            transform.localScale -= new Vector3(difference, 0f, 0f);

            // create box cutaway on the left
            var positionX = leftLow - (difference / 2);
            var positionY = boxUpperPosition.y < CUTAWAY_OFFSET ? boxUpperPosition.y : boxUpperPosition.y - boxUpperSize.y;

            var positionVector = new Vector2(positionX, positionY);
            var cutawayScale = new Vector3(difference, 1, 1);
            BoxCutaway.createBoxCutaway(positionVector, cutawayScale, LEFT);
        }

        // if upper box is further to the right than lower box
        if (rightLow < rightUpp) {
            var difference = rightUpp - rightLow;

            // shift upper box right and decrease the horizontal scale
            transform.position -= new Vector3(difference / 2, 0f, 0f);
            transform.localScale -= new Vector3(difference, 0f, 0f);

            // create box cutaway on the right
            var positionX = rightLow + (difference / 2);
            var positionY = boxUpperPosition.y < CUTAWAY_OFFSET ? boxUpperPosition.y : boxUpperPosition.y - boxUpperSize.y;

            var positionVector = new Vector2(positionX, positionY);
            var cutawayScale = new Vector3(difference, 1, 1);
            BoxCutaway.createBoxCutaway(positionVector, cutawayScale, RIGHT);
        }
    }
}
