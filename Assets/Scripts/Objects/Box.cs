using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Box : MonoBehaviour
{
    const int SPEED = 2;
    const int LEFT = 1;
    const int RIGHT = -1;
    const float CUTAWAY_POSITION = -3.5f;
    const float CUTAWAY_OFFSET = 0.5f;

    Rigidbody2D rigidbodyComponent;
    int direction;
    Vector2 movementVectorH;
    Vector3 movementVectorV = Vector3.zero;
    Vector3 initialPosition;
    Vector3 targetPosition;
    Vector3 velocity = Vector3.zero;
    float time = 0.05f;
    int boxHeightFromCentre;

    // FIX IF NEEDED
    int cameraBottom = -5;
    public static Color boxColor = new Color(255, 255, 0);

    public static Box createBox(GameObject prefab, Vector2 position, Vector3 scale) {
        var newObject = Instantiate(prefab, position, Quaternion.identity);
        var spriteRenderer = newObject.GetComponent<SpriteRenderer>();
        spriteRenderer.color = boxColor;
        boxColor = new Color(255, 0, 0);
        newObject.transform.localScale = scale;
        return newObject.GetComponent<Box>();
    }

    void destroyBox() {
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start() {
        rigidbodyComponent = GetComponent<Rigidbody2D>();
        boxHeightFromCentre = (int)getSize().y / 2;

        // random direction
        System.Random random = new System.Random();
        direction = random.Next(0, 2) == 0 ? -1 : 1;
        movementVectorH = new Vector2(SPEED, 0);
    }

    // called 100 times a second
    void FixedUpdate() {
        int top = (int)getCentre().y + boxHeightFromCentre;
        if (top <= cameraBottom) {
           destroyBox();
        }

        rigidbodyComponent.velocity = movementVectorH * direction;
        if (movementVectorV != Vector3.zero) {
            var currentPosition = getCentre();
            if (currentPosition != targetPosition) {
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, time);
            } else {
                movementVectorV = Vector3.zero;
            }
        }
    }

    // on collision with a wall reverse direction
    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "MainCamera") {
            direction *= -1;
        }
    }

    // stops the box's movement
    public void stopMovement() {
        this.direction = 0;
    }

    // get Vector3 with the centre of the box
    public Vector3 getCentre() {
        return transform.position;
    }

    // get Vector2 with the dimentions of the box
    public Vector3 getSize() {
        return transform.localScale;
    }

    public void moveBox(Vector3 direction) {
        movementVectorV = direction;
        initialPosition = getCentre();
        targetPosition = initialPosition + direction;
    }

    public void cutBox(Box boxLower, GameObject cutawayPrefab) {
        // get size of boxes in Vector2(size_x, size_y)
        Vector3 boxLowerSize = boxLower.getSize();
        Vector3 boxUpperSize = this.getSize();
        
        // get centre and world positions in Vector3
        Vector3 boxLowerPosition = boxLower.getCentre();
        Vector3 boxUpperPosition = this.getCentre();

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
            BoxCutaway.createBoxCutaway(cutawayPrefab, positionVector, cutawayScale, LEFT);
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
            BoxCutaway.createBoxCutaway(cutawayPrefab, positionVector, cutawayScale, RIGHT);
        }
    }
}
