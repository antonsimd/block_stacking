using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCutaway : MonoBehaviour
{

    const int CAMERA_BOTTOM = -6;
    const int LEFT = 1;
    const int RIGHT = -1;

    float rotation = 0.2f;
    float speed = 0.35f;
    float opacity = 0.035f;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rigidbodyComponent;

    Color opacityChange;
    Vector3 rotationVector;

    void Start () {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        rigidbodyComponent = GetComponent<Rigidbody2D>();

        rotationVector = new Vector3(0, 0, rotation);

        opacityChange = new Color(0, 0, 0, opacity);

        rigidbodyComponent.velocity += new Vector2(speed, 0);
    }

    public static BoxCutaway createBoxCutaway(GameObject prefab, Vector2 position, Vector3 scale, int direction) {
        position.y -= 0.5f;
        var newObject = Instantiate(prefab, position, Quaternion.identity);
        newObject.transform.localScale = scale;

        var component = newObject.GetComponent<BoxCutaway>();
        component.setDirection(direction);
        return component;
    }

    public void setDirection(int direction) {
        rotation *= direction;

        // direction is opposite to rotation;
        speed *= - 1 * direction;
    }

    // Start is called before the first frame update
    void FixedUpdate() {

        spriteRenderer.color -= opacityChange;

        transform.Rotate(rotationVector, Space.Self);

        if (transform.position.y < CAMERA_BOTTOM) {
            Destroy(gameObject);
        }
    }
}