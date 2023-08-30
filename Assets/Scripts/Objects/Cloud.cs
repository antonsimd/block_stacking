using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    // FIX IF NEEDED
    static Vector4 cameraDimensions = new Vector4(-2.5f, 2.5f, -5f, 5f);
    const int SPAWN_CLOUDS = 15;
    // Percentage chance of spawning a cloud
    const int SPAWN_CLOUD_CHANCE = 10;

    static List<Cloud> clouds = new List<Cloud>();
    static System.Random random = new System.Random();

    bool moveNeeded = false;
    Vector3 initialPosition;
    Vector3 targetPosition;
    Vector3 velocity = Vector3.zero;
    Vector3 MOVEMENT_AMOUNT = new Vector3(0, -0.25f, 0);
    float time = 0.05f;


    public static void clearClouds() {
        clouds.Clear();
    }

    // Move all clouds down and create a new cloud if with a given change
    public static void moveUp() {
        if (Score.currentScore.getScore() > SPAWN_CLOUDS) {
            int chance = random.Next(0, 100);
            
            foreach (Cloud cloud in clouds) {
                cloud.moveCloud();
                if ((int)cloud.getCentre().y < cameraDimensions.z) {
                    cloud.destroyCloud();
                    clouds.Remove(cloud);
                }
            }

            if (chance < SPAWN_CLOUD_CHANCE) {
                clouds.Add(spawnCloud());
            }
        }
    }

    // Instantiate a cloud at the top of the screen with a random x position
    static Cloud spawnCloud() {
        var positionX = random.Next(-25, 25);
        positionX = positionX / 10;
        Vector2 position = new Vector2(positionX, cameraDimensions.w);

        var newObject = Instantiate(MainBody.mainBody.cloudPrefab, position, Quaternion.identity);
        var script = newObject.GetComponent<Cloud>();

        return script;
    }

    // Moves the cloud if needed
    void FixedUpdate() {
        if (moveNeeded) {
            var currentPosition = getCentre();
            if (currentPosition != targetPosition) {
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, time);
            } else {
                moveNeeded = false;
            }
        }
    }

    // Sets move to needed and the target direction for the move
    void moveCloud() {
        moveNeeded = true;
        initialPosition = getCentre();
        targetPosition = initialPosition + MOVEMENT_AMOUNT;
    }


    void destroyCloud() {
        Destroy(gameObject);
    }

    Vector3 getCentre() {
        return transform.position;
    }
}
