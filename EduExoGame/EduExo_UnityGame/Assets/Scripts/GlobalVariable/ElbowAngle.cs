using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.IO;
using System;

public class ElbowAngle : MonoBehaviour
{
    private SerialPort sp;

    //[SerializeField]
    //private AnimationTransformer[] animationTransformers;

    [SerializeField]
    private ArmBehaviour arm;

    [SerializeField]
    private float elbowangle = 0;

    [SerializeField]
    private int port;

    private byte[] buffer = new byte[1];

    private void OnEnable()
    {
        sp = new SerialPort("COM"+port, 9600);
        sp.ReadTimeout = 1000;
        try
        {
            sp.Open();
        } catch(System.Exception e)
        {
            Debug.LogWarning(e);
        }
    }

    private void OnDisable()
    {
        sp.Close();
    }

    private void ReadUSBPort()
    {
        if(sp.IsOpen)
        {
            while (sp.ReadByte() != 0xFF) ;
            try
            {
                elbowangle = sp.ReadByte() + 256 * sp.ReadByte();

                if(elbowangle < 90)
                {
                    buffer[0] = 0;
                } else if(elbowangle > 180)
                {
                    buffer[0] = 1;
                }

                sp.Write(buffer, 0, 1);
            } catch(System.Exception e)
            {
                Debug.LogError(e);
            }
        }
    }

    public void Update()
    {
        ReadUSBPort();
        /*foreach(AnimationTransformer at in animationTransformers)
        {
            at.UpdateTransformation(elbowangle);
        }*/
        arm.SetAngle(elbowangle);
    }
}
