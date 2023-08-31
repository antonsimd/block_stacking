using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BoxRevision : MonoBehaviour
{
    // Constants
    const int SPEED = 2;
    const int LEFT = 1;
    const int RIGHT = -1;
    const float CUTAWAY_OFFSET = 0.5f;

    // Game Object and game related
    Rigidbody2D rigidbodyComponent;
    static List<BoxRevision> boxes = new List<BoxRevision>();
    static System.Random random = new System.Random();

    // FIX IF NEEDED
    static Vector4 cameraDimensions = new Vector4(-2.5f, 2.5f, -5f, 5f);

    void Start() {
        rigidbodyComponent = GetComponent<Rigidbody2D>();
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
