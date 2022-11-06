using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftArmToggleChanged : MonoBehaviour
{
    public void OnValueChanged(bool value)
    {
        TechnicalData.leftArm = value;
    }
}
