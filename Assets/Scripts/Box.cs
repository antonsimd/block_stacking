using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Box : MonoBehaviour
{
    const int SPEED = 2;

    private Rigidbody2D rigidbodyComponent;
    private int direction;
    private Vector2 movementVector;
    
    // Start is called before the first frame update
    private void Start() {
        rigidbodyComponent = GetComponent<Rigidbody2D>();

        // random direction
        System.Random random = new System.Random();
        direction = random.Next(0, 2) == 0 ? -1 : 1;
        
        movementVector = new Vector2(SPEED, 0);
    }

    // Called every frame
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            direction = 0;
        }
    }

    // called 100 times a second
    private void FixedUpdate() {
        rigidbodyComponent.velocity = movementVector * direction;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "MainCamera") {
            direction *= -1;
        }
    }
}
