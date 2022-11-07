using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
        if(remainingSeconds <= 0)
        {
            remainingSeconds = 0;
            SceneManager.LoadScene(0);
        }
        text.SetText(TimeReader.SecondsToTime(remainingSeconds));
    }
}
