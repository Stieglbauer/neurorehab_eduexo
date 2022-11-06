using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStarter : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objectsToDeactivate;

    [SerializeField]
    private GameObject[] objectsToActivate;

    [SerializeField]
    private TMP_InputField timeLimit, spawnRate;

    public void StartGame()
    {
        if(timeLimit.text != "")
        {
            TechnicalData.timeLimit = TimeReader.TimeToSeconds(timeLimit.text);
        }
        if(spawnRate.text != "")
        {
            TechnicalData.spawnRate = TimeReader.TimeToSeconds(spawnRate.text);
        }

        foreach(var obj in objectsToDeactivate)
        {
            obj.SetActive(false);
        }
        foreach (var obj in objectsToActivate)
        {
            obj.SetActive(true);
        }
    }
}
