using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{

    public static Ground instance;

    private Vector3 movementVector = Vector3.zero;
    private Vector3 velocity = Vector3.zero;
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private float time = 0.05f;
    private BoxCollider2D boxCollider;
    // Start is called before the first frame update
    private void Start() {
        instance = this;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void FixedUpdate() {
        if (movementVector != Vector3.zero) {
            var currentPosition = transform.TransformPoint(boxCollider.offset);
            if (currentPosition != targetPosition) {
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, time);
            } else {
                movementVector = Vector3.zero;
            }
        }
    }

    public void moveGround(Vector3 direction) {
        movementVector = direction;
        initialPosition = transform.TransformPoint(boxCollider.offset);
        targetPosition = initialPosition + direction;
    }
}
