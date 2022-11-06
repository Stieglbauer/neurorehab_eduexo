using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReformatTimeString : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField textField;

    public void ReformatInput(string input)
    {
        textField.SetTextWithoutNotify(TimeReader.SecondsToTime(TimeReader.TimeToSeconds(input)));
    }
}
