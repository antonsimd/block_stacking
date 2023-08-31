using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BoxRevision : MonoBehaviour
{
    // Constants
    const int SPEED = 2;
    const int INITIAL_BOX_Y_OFFSET = -4;
    const int LEFT = 1;
    const int RIGHT = -1;
    const float CUTAWAY_OFFSET = 0.5f;
    const int BOXES_MOVE_DOWN = 4;
    const float ySpeed = 2f

    // Game Object and game related
    Rigidbody2D rigidbodyComponent;
    Vector3 boxInitialScale = new Vector3(2.5f, 1f, 1f);
    int direction;
    bool moveRequired = false;
    Vector3 targetPosition;
    Vector2 movementVectorH = new Vector2(SPEED, 0);

    // Static variable for storing all boxes
    static List<BoxRevision> boxes = new List<BoxRevision>();
    static System.Random random = new System.Random();
    static Vector2 boxInitialPosition = new Vector2(0, INITIAL_BOX_Y_OFFSET);
    static int boxCount = 0;

    // FIX IF NEEDED
    static Vector4 cameraDimensions = new Vector4(-2.5f, 2.5f, -5f, 5f);

    void Start() {
        rigidbodyComponent = GetComponent<Rigidbody2D>();
        direction = random.Next(0, 2) == 0 ? -1 : 1;
        direction = Box.boxCount == 1 ? 0 : direction;
    }

    void Update() {
        if (moveRequired && transform.position != targetPosition) {
            transform.position = Vector3.MoveTowards(transform.position,, targetPosition, ySpeed * Time.deltaTime);
        } else {
            moveRequired == false;
        }

        rigidbodyComponent.velocity = movementVectorH * direction * Time.deltaTime;
    }

    public static void drawInitialBox() {
        var newObject = Instantiate(MainBody.mainBody.boxPrefab, boxInitialPosition, Quaternion.identity);
        var script = newObject.GetComponent<BoxRevision>();

        boxes.Add(script);
        boxCount++;
    }

    public static void moveUp() {
        if (boxCount == 0) {
            drawInitialBox();
        } else if (boxCount <= BOXES_MOVE_DOWN) {
            
        }
    }

    void stopMovement() {
        direction = 0;
    }

    static void createBox(Vector2 position, Vector3 scale) {
        var newObject = Instantiate(MainBody.mainBody.boxPrefab, position, Quaternion.identity);
        var script = newObject.GetComponent<BoxRevision>();

        newObject.transform.localScale = scale;

        boxes.Add(script);
        boxCount++;
    }

    void moveBox() {
        var initialPosition = getCentre();
        targetPosition = initialPosition - getSize().y;
        moveRequired = true;
    }

    void destroyBox() {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision) {
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
}
