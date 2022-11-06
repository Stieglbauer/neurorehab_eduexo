using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject carPrefab;

    private List<PeopleBehaviour> peoples;

    private WaitForSeconds waitTime;

    public void Start()
    {
        waitTime = new WaitForSeconds(TechnicalData.spawnRate);

        peoples = new List<PeopleBehaviour>();
        InstantiateNewCar();

        StartCoroutine(Spawn());
    }

    private PeopleBehaviour InstantiateNewCar()
    {
        var newCar = Instantiate(carPrefab).GetComponent<PeopleBehaviour>();
        peoples.Insert(0, newCar);
        return newCar;
    }

    private void SpawnCar()
    {
        var car = peoples[peoples.Count - 1];
        if (car.currentState == PeopleBehaviour.PeopleState.spawning)
        {
            peoples.RemoveAt(peoples.Count - 1);
        } else
        {
            car = InstantiateNewCar();
        }

        car.Ini();
        peoples.Insert(0, car);
    }

    private IEnumerator Spawn()
    {
        while(true)
        {
            SpawnCar();

            yield return waitTime;
        }
    }
}
