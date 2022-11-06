using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeReader : MonoBehaviour
{
    public static int TimeToSeconds(string timeString)
    {
        string[] subStrings = timeString.Split(":");
        int idx = 0, result = 0;
        if(subStrings.Length > 1)
        {
            result = int.Parse(subStrings[idx++]) * 60;
        }
        result += int.Parse(subStrings[idx]);

        Debug.Log(result);

        return result;
    }

    public static string SecondsToTime(int seconds)
    {
        int minutes = seconds / 60;

        return string.Format("{0}:{1:D2}", minutes, seconds % 60);
    }
}
