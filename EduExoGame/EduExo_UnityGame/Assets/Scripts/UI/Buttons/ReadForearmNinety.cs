using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReadForearmNinety : MonoBehaviour
{
    [SerializeField]
    private ElbowAngle elbowAngle;

    [SerializeField]
    private TMP_InputField text;

    public void OnRead()
    {
        bool fail = false;
        try
        {
            fail = !elbowAngle.ReadUSBPort();
            TechnicalData.forearm90 = elbowAngle.elbowangle;
            text.SetTextWithoutNotify("" + TechnicalData.forearm90);
        }
        catch (System.Exception e)
        {
            fail = true;
            Debug.LogError(e);
        }

        if (fail)
        {
            text.SetTextWithoutNotify("---");
        }
    }
}
