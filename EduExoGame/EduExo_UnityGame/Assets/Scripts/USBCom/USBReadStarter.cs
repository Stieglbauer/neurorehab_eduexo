using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class USBReadStarter : MonoBehaviour
{
    [SerializeField]
    private ElbowAngle usbReader;

    private void Awake()
    {
        usbReader.SetPort(TechnicalData.usbPort);
    }
}
