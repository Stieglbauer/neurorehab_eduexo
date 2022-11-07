using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Logger : MonoBehaviour
{

    private StreamWriter writer;

    private void OnEnable()
    {

        string path = "Assets/test"+System.DateTime.Now.Year+"-"+System.DateTime.Now.Month+"-"+System.DateTime.Now.Day+"--"+System.DateTime.Now.Hour+"-"+System.DateTime.Now.Minute+".txt";

        //Write some text to the test.txt file

        writer = new StreamWriter(path, true);
        writer.WriteLine("UA-Ange: " + TechnicalData.upperArmAngle + ", ForearmStretched: " + TechnicalData.forearmStretched + ", Forearm: " + TechnicalData.forearm90 + ", LeftArm? " + (TechnicalData.leftArm ? "Yes" : "No"));
        writer.WriteLine("Time, EMG-Signal, Force-Value, Elbow-Angle, Transformed-Angle, MotorTarget");
    }

    private void OnDisable()
    {
        writer.Close();
    }

    public void Log(float emgSensor, float forceSensor, float angle, float transformedAngle, int servoMotor)
    {
        writer.WriteLine(Time.time + ", " + emgSensor + ", " + forceSensor + ", " + angle + ", " + transformedAngle + ", " + servoMotor);
        Debug.Log(emgSensor + ", " + forceSensor + ", " + angle + ", " + transformedAngle + ", " + servoMotor);
    }

    public void Log(string s)
    {
        writer.WriteLine(s);
    }
}
