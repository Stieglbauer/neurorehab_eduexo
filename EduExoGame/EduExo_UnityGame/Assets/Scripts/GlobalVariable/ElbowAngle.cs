using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElbowAngle : MonoBehaviour
{
    [SerializeField]
    private AnimationTransformer[] animationTransformers;

    // debug
    public float elbowangle = 0;
    public void Update()
    {
        foreach(AnimationTransformer at in animationTransformers)
        {
            at.UpdateTransformation(elbowangle);
        }
    }
}
