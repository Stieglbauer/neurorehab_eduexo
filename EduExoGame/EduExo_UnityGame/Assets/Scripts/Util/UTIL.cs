using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UTIL
{
    public static Vector3 LerpAbsDistance(Vector3 positionA, Vector3 positionB, float distance)
    {
        return Mathf.Max(0, distance) > Vector3.Distance(positionA, positionB)?
            positionB:
            positionA + Mathf.Max(0, distance) * Vector3.Normalize(positionB - positionA);
    }
}
