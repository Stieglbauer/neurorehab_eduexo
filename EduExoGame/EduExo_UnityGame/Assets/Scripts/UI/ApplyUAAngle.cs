using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ApplyUAAngle : MonoBehaviour
{
    [SerializeField]
    private Transform armTemplate;

    [SerializeField]
    private TMP_InputField text;

    public void OnValueChanged()
    {
        if(float.TryParse(text.text, out TechnicalData.upperArmAngle))
        {
            armTemplate.rotation = Quaternion.Euler(0, 0, TechnicalData.upperArmAngle);
        } else
        {
            text.text = "";
        }
    }
}
