using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Box : MonoBehaviour
{
    const int SPEED = 2;

    private Rigidbody2D rigidbodyComponent;
    private BoxCollider2D boxCollider;
    private int direction;
    private Vector2 movementVector;
    public static Box instance;

    // Start is called before the first frame update
    private void Start() {
        rigidbodyComponent = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        // random direction
        System.Random random = new System.Random();
        direction = random.Next(0, 2) == 0 ? -1 : 1;
        
        movementVector = new Vector2(SPEED, 0);
    }

    // stops the box's movement
    public void stopMovement() {
        direction = 0;
    }

    // called 100 times a second
    private void FixedUpdate() {
        rigidbodyComponent.velocity = movementVector * direction;
    }

    // on collision with a wall reverse direction
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "MainCamera") {
            direction *= -1;
        }
    }

    // get Vector3 with the centre of the box
    public Vector3 getCentre() {
        return transform.TransformPoint(boxCollider.offset);
    }

    // get Vector2 with the dimentions of the box
    public Vector2 getSize() {
        return boxCollider.size;
    }
}
