using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTransformer : AnimationTransformer
{
   
    protected Quaternion initialRotation;
    protected Vector3 initialPosition;

    [SerializeField]
    protected float updateFactor = 1;

    private void Awake()
    {
        initialRotation = transform.rotation;
        initialPosition = transform.localPosition;
    }

    public override void UpdateTransformation(float progress)
    {
        transform.rotation = initialRotation;
        transform.Rotate(progress * updateFactor * Vector3.forward);
    }
}
