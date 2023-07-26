using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCutaway : MonoBehaviour
{

    const int CAMERA_BOTTOM = -6;
    const int LEFT = 1;
    const int RIGHT = -1;

    float rotation = 0.1f;

    Color opacityChange = new Color(0, 0, 0, 0.035f);
    Vector3 rotationVector;
    SpriteRenderer spriteRenderer;

    void Start () {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        rotationVector = new Vector3(0, 0, rotation);
    }

    public static BoxCutaway createBoxCutaway(GameObject prefab, Vector2 position, Vector3 scale, int direction) {
        var newObject = Instantiate(prefab, position, Quaternion.identity);
        newObject.transform.localScale = scale;

        var component = newObject.GetComponent<BoxCutaway>();
        component.setDirection(direction);
        return component;
    }

    public void setDirection(int direction) {
        rotation *= direction;
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
