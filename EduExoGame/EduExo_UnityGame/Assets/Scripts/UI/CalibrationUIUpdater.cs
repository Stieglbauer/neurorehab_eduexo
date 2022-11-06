using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CalibrationUIUpdater : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField portTF, uaAngleTF, faStretchedTF, fa90TF;

    [SerializeField]
    private Toggle leftArmToggle;

    public void Start()
    {
        portTF.SetTextWithoutNotify("" + TechnicalData.usbPort);
        uaAngleTF.SetTextWithoutNotify("" + TechnicalData.upperArmAngle);
        faStretchedTF.SetTextWithoutNotify("" + TechnicalData.forearmStretched);
        fa90TF.SetTextWithoutNotify("" + TechnicalData.forearm90);
        leftArmToggle.isOn = TechnicalData.leftArm;
    }
}
