using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDown : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;

    private float startTime;

    private void Awake()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        int remainingSeconds = Mathf.CeilToInt(TechnicalData.timeLimit - (Time.time - startTime));
        text.SetText(TimeReader.SecondsToTime(remainingSeconds));
    }
}
