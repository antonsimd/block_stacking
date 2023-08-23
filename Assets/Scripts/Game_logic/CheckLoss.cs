using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLoss
{
    public static bool checkLoss(Box boxUpper, Box boxLower) {
        
        if (boxUpper == null || boxLower == null) {
            return false;
        }
        // get size of boxes in Vector2(size_x, size_y)
        Vector3 boxLowerSize = boxLower.getSize();
        Vector3 boxUpperSize = boxUpper.getSize();
        
        // get centre and world positions in Vector3
        Vector3 boxLowerPosition = boxLower.getCentre();
        Vector3 boxUpperPosition = boxUpper.getCentre();

        // calculate left and right boundaries of both boxes based on centre and size
        float leftLow = boxLowerPosition.x - (boxLowerSize.x / 2f);
        float leftUpp = boxUpperPosition.x - (boxUpperSize.x / 2f);
        float rightLow = boxLowerPosition.x + (boxLowerSize.x / 2f);
        float rightUpp = boxUpperPosition.x + (boxUpperSize.x / 2f);

        if (rightUpp < leftLow || rightLow < leftUpp) {
            GameOver.instance.gameOver();
            return true;
        } else {
            return false;
        }
    }
}
