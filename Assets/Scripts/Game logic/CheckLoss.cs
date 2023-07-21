using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLoss
{
    public static bool checkLoss(Box boxUpper, Box boxLower) {
        var boxLowerCollider = boxLower.GetComponent<EdgeCollider2D>();
        var boxLowerRight = boxLowerCollider.size;

        return false;
    }
}
