using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReadTransformedAngle : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputField;

    [SerializeField]
    private ElbowAngle elbowAngle;

    [SerializeField]
    private GameSetup setup;

    [SerializeField]
    private bool leftSegment;

    public void OnRead()
    {
        inputField.text = - ElbowAngle.TransformAngle(elbowAngle.elbowangle) + "";
        setup.SetupSegment(leftSegment);
    }

    public void OnInput(string input)
    {
        setup.SetupSegment(leftSegment, float.Parse(input));
    }
}
