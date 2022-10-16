using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimationTransformer : MonoBehaviour
{
    public abstract void UpdateTransformation(float progress);
}
