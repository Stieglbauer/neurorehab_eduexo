using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointCounter : MonoBehaviour
{
    private int points = 0;

    [SerializeField]
    private TMP_Text pointDisplay;

    public void AddPoint()
    {
        pointDisplay.text = "Points: " + points++;
    }
}
