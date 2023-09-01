using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{

    const int MOVE_DOWN = 3;
    const int GROUND_Y_OFFSET = -5;
    Vector3 movementVector = Vector3.zero;
    Vector3 velocity = Vector3.zero;
    Vector3 initialPosition;
    Vector3 targetPosition;
    float time = 0.05f;
    BoxCollider2D boxCollider;

    // FIX IF NEEDED
    int cameraBottom = -5;

    static Vector2 groundPosition = new Vector2(0, GROUND_Y_OFFSET);

    public static void createGround() {
        var newObject = Instantiate(MainBody.mainBody.groundPrefab, groundPosition, Quaternion.identity);
    }

    void destroyGround() {
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start() {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update() {
        if (Input.touchCount > 0 && Score.currentScore.getScore() >= MOVE_DOWN) {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began) {
                moveGround(Vector3.down);
            }
        }
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
