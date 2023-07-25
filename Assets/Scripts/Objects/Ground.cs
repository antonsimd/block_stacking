using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    Vector3 movementVector = Vector3.zero;
    Vector3 velocity = Vector3.zero;
    Vector3 initialPosition;
    Vector3 targetPosition;
    float time = 0.05f;
    BoxCollider2D boxCollider;

    // FIX IF NEEDED
    int cameraBottom = -5;

    public static Ground createGround(GameObject prefab, Vector2 position) {
        var newObject = Instantiate(prefab, position, Quaternion.identity);
        return newObject.GetComponent<Ground>();
    }

    void destroyGround() {
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start() {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (transform.position.y < cameraBottom) {
            destroyGround();
        }
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
