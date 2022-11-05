using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformDistanceEnterCondition : EnterCondition
{
    [SerializeField]
    private Transform transformA, transformB;

    [SerializeField]
    private float minDistance;

    private float minDistanceSquared;

    private void Awake()
    {
        minDistanceSquared = minDistance * minDistance;
    }

    public override bool Eval()
    {
        float deltaX = transformA.position.x - transformB.position.x;
        float deltaY = transformA.position.y - transformB.position.y;
        return deltaX * deltaX + deltaY * deltaY <= minDistanceSquared;
    }
}
