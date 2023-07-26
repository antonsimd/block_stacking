using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCutaway : MonoBehaviour
{

    int cameraBottom = -6;
    Color opacityChange = new Color(0,0,0,0.035f);

    SpriteRenderer spriteRenderer;

    void Start () {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public static BoxCutaway createBoxCutaway(GameObject prefab, Vector2 position, Vector3 scale) {
        var newObject = Instantiate(prefab, position, Quaternion.identity);
        newObject.transform.localScale = scale;
        return newObject.GetComponent<BoxCutaway>();
    }

    // Start is called before the first frame update
    void FixedUpdate() {

        spriteRenderer.color -= opacityChange;

        if (transform.position.y < cameraBottom) {
            Destroy(gameObject);
        }
    }
}
