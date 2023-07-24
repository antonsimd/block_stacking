using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Box : MonoBehaviour
{
    const int SPEED = 2;

    public static Box instance;

    private Rigidbody2D rigidbodyComponent;
    private BoxCollider2D boxCollider;
    private int direction;
    private Vector2 movementVectorH;
    private Vector3 movementVectorV = Vector3.zero;
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;
    private float time = 0.05f;

    // Start is called before the first frame update
    private void Start() {
        instance = this;
        rigidbodyComponent = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        // random direction
        System.Random random = new System.Random();
        direction = random.Next(0, 2) == 0 ? -1 : 1;
        movementVectorH = new Vector2(SPEED, 0);
    }

    // stops the box's movement
    public void stopMovement() {
        direction = 0;
    }

    // called 100 times a second
    private void FixedUpdate() {
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
        return instance.transform.localScale;
    }

    public void moveBox(Vector3 direction) {
        movementVectorV = direction;
        initialPosition = getCentre();
        targetPosition = initialPosition + direction;
    }
}
