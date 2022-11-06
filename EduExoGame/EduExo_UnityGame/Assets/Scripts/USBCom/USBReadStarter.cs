using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class USBReadStarter : MonoBehaviour
{
    [SerializeField]
    private ElbowAngle usbReader;

    private void Awake()
    {
        //DEBUG
        TechnicalData.usbPort = 8;

        usbReader.SetPort(TechnicalData.usbPort);
    }
}
