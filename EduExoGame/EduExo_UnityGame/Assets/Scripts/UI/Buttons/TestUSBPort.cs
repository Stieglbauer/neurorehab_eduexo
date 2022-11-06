using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestUSBPort : MonoBehaviour
{
    [SerializeField]
    private ElbowAngle elbowAngleReader;

    [SerializeField]
    private Image bg;

    [SerializeField]
    private Color colorGood, colorBad;

    public void ApplyUSBPort(string text)
    {
        TechnicalData.usbPort = int.Parse(text);
        if(elbowAngleReader.SetPort(TechnicalData.usbPort))
        {
            bg.color = colorGood;
        } else
        {
            bg.color = colorBad;
        }
    }
}
