using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollisionBox : MonoBehaviour
{

    Camera cam;
    private void Awake() {
        cam = Camera.main;
        addCollider();
    }

    private void addCollider() {
        if (Camera.main == null) {
            Debug.LogError("No camera found");
        }

        if (!cam.orthographic) {
            Debug.LogError("Set camera to orthographic");
        }

        // get or add edge collider 2D componend
        EdgeCollider2D edgeCollider = gameObject.GetComponent<EdgeCollider2D>() == null ? 
            gameObject.AddComponent<EdgeCollider2D>() : gameObject.GetComponent<EdgeCollider2D>();

        
        Vector2 leftBottom = cam.ScreenToWorldPoint(new Vector2(0, 0));
        Vector2 leftTop = cam.ScreenToWorldPoint(new Vector2(0, cam.pixelHeight));
        Vector2 rightBottom = cam.ScreenToWorldPoint(new Vector2(cam.pixelWidth ,0));
        Vector2 rightTop = cam.ScreenToWorldPoint(new Vector2(cam.pixelWidth, cam.pixelHeight));

        // create a border for the edge collider
        Vector2[] edgePoints = new[] { leftBottom, leftTop, rightTop, rightBottom, leftBottom };
        edgeCollider.points = edgePoints;
    }
}
