using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBody : MonoBehaviour
{
    // constants
    const int INITIAL_BOX_Y_OFFSET = -4;
    const int GROUND_Y_OFFSET = -5;

    // prefabs
    public GameObject boxPrefab;
    public GameObject groundPrefab;

    // instances of game objects
    private GameObject box1;
    private GameObject ground;

    // box position offsets
    private Vector2 boxInitialPosition = new Vector2(0, INITIAL_BOX_Y_OFFSET);
    private Vector2 groundPostition = new Vector2(0, GROUND_Y_OFFSET);
    
    // draw the box before rendering the first frame, avoids lag
    private void Awake() {
        box1 = Instantiate(boxPrefab, boxInitialPosition, Quaternion.identity);
        ground = Instantiate(groundPrefab, groundPostition, Quaternion.identity);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            box1.GetComponent<Box>().stopMovement();
        }
    }
}
