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
    private Logger logger;

    [SerializeField]
    private ArmBehaviour arm;

    public float elbowangle { get; private set; } = 0;

    public bool SetPort(int port)
    {
        sp?.Close();
        sp = new SerialPort("COM" + port, 9600);
        sp.ReadTimeout = 1000;
        try
        {
            sp.Open();
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e);
            return false;
        }
        return true;
    }

    private byte[] buffer = new byte[1];

    private void OnDisable()
    {
        sp?.Close();
    }

    public bool ReadUSBPort()
    {
        if(sp != null && sp.IsOpen)
        {
            while (sp.ReadByte() != 0xFF) ;
            try
            {
                elbowangle = sp.ReadByte() + 256 * sp.ReadByte();
                int forceSensor = sp.ReadByte() + 256 * sp.ReadByte();
                int emgSignal = sp.ReadByte() + 256 * sp.ReadByte();
                int motorTarget = sp.ReadByte() + 256 * sp.ReadByte();
                float transformedAngle = TransformAngle(elbowangle);


                logger?.Log(emgSignal, forceSensor, elbowangle, transformedAngle, motorTarget);

                /*if(elbowangle < 90)
                {
                    buffer[0] = 0;
                } else if(elbowangle > 180)
                {
                    buffer[0] = 1;
                }

                sp.Write(buffer, 0, 1);*/
                return true;
            } catch(System.Exception e)
            {
                Debug.LogError(e);
                return false;
            }
        } else
        {
            return false;
        }
    }

    public void Update()
    {
        ReadUSBPort();
        arm?.SetAngle(TransformAngle(elbowangle));
    }

    public static float TransformAngle(float rawAngle)
    {
        return ((rawAngle - TechnicalData.forearmStretched) / (TechnicalData.forearm90 - TechnicalData.forearmStretched)) * (-90.0f);// + 23.0f;
    }
}
