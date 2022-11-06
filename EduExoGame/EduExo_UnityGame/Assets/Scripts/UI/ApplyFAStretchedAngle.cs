using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ApplyFAStretchedAngle : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField text;

    public void OnValueChanged()
    {
        if (!float.TryParse(text.text, out TechnicalData.upperArmAngle))
        {
            text.text = "";
        }
    }
}
