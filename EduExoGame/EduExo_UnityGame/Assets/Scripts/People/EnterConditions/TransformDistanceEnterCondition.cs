using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformDistanceEnterCondition : EnterCondition
{
    [SerializeField]
    private Transform transformA, transformB;

    [SerializeField]
    private float minDistance;

    public override bool Eval()
    {
        return Vector3.Distance(transformA.position, transformB.position) <= minDistance;
    }
}
