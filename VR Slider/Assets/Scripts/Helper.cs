using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper
{
    public static Vector3 GetHandDirection(Vector3 snapVec, Vector3 curVec)
    {
        return snapVec - curVec;
    }
}
