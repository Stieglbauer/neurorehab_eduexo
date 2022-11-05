using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleBehaviour : MonoBehaviour
{
    private Transform waypointA, waypointB;

    private float trackProgress = 0, trackDistance;

    private const float trackSpeed = 2.0f;

    public PeopleState currentState { get; private set; } = PeopleState.spawning;

    [SerializeField]
    private GameObject[] sprites;

    public enum PeopleState
    {
        // game states
        //waiting,
        running,

        // out-of-game state
        spawning,
    }

    private int trackSegment = 0;

    /// <summary>
    /// Follow the track #trackSegment between waypoint A and B
    /// </summary>
    private void running()
    {
        if (PeopleReferences.GetQueueIndex(trackSegment, this) == 0 && trackProgress >= 1 && PeopleReferences.RequestQueueEnd(trackSegment+1, this))
        {
            EnterSegment(++trackSegment);

            if (currentState == PeopleState.spawning)
            {
                return;
            }
        }

        this.transform.position = Vector3.Lerp(waypointA.position, waypointB.position, trackProgress);
        this.transform.rotation = Quaternion.LookRotation(
            new Vector3(waypointB.position.x - waypointA.position.x, waypointB.position.y - waypointA.position.y, 0),
            Vector3.up);

        Vector3 queuePos = PeopleReferences.GetQueuePosition(trackSegment, PeopleReferences.GetQueueIndex(trackSegment, this));
        if (Vector3.Distance(waypointA.position, transform.position) > Vector3.Distance(waypointA.position, queuePos))
        {
            this.transform.position = queuePos;
        } else
        {
            trackProgress += trackSpeed * Time.deltaTime / trackDistance;
        }
    }

    private void SwitchToState(PeopleState newState)
    {
        currentState = newState;
    }

    private void ActOnState()
    {
        switch(currentState)
        {
            case PeopleState.running:
                running();
                break;
        }
    }

    /// <summary>
    /// Reset / Initialize the PeopleBehaviour object -> Can be reused after reaching the goal
    /// </summary>
    public void Ini()
    {
        EnterSegment(0);

        currentState = PeopleState.running;

        int rand = Random.Range(0, sprites.Length);

        foreach(var sprite in sprites)
        {
            sprite.SetActive(false);
        }

        sprites[rand].SetActive(true);
    }

    private void EnterSegment(int newSegment)
    {
        if(PeopleReferences.GetWaypoints(newSegment, ref waypointA, ref waypointB))
        {
            SwitchToState(PeopleState.spawning);
        }
        trackDistance = Vector3.Distance(waypointA.position, waypointB.position);
        trackProgress = 0;
        trackSegment = newSegment;
        this.transform.position = waypointA.position;
    }

    private void Update()
    {
        ActOnState();
    }
}
