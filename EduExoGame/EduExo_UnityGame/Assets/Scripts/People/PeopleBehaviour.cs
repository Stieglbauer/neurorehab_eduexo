using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleBehaviour : MonoBehaviour
{
    private int queuePosition;

    private Transform waypointA, waypointB;

    public float trackProgress = 0, trackDistance, trackSpeed = 2.0f;

    public PeopleState currentState;

    public enum PeopleState
    {
        // game states
        waiting,
        running,
        cheering,
        flying,
        falling,

        // out-of-game state
        spawning,
    }

    private int trackSegment = 0;

    /// <summary>
    /// Stay on the current position
    /// </summary>
    private void waiting()
    {
        // stay on position
    }

    /// <summary>
    /// Follow the track #trackSegment between waypoint A and B
    /// </summary>
    private void running()
    {
        if (trackProgress > 1)
        {
            SwitchToState(PeopleState.waiting);
            return;
        }
        this.transform.position = Vector3.Lerp(waypointA.position, waypointB.position, trackProgress);
        trackProgress += trackSpeed * Time.deltaTime / trackDistance;
    }

    private void cheering()
    {

    }

    private void flying()
    {

    }

    private void falling()
    {

    }

    private void SwitchToState(PeopleState newState)
    {
        currentState = newState;
        if(newState == PeopleState.waiting)
        {
            transform.position = waypointB.position;
            EnterSegment(++trackSegment);
        }
    }

    private void FindState()
    {
        //currentState = PeopleState.running;
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
    private void Ini()
    {
        EnterSegment(0);

        currentState = PeopleState.running;
    }

    private void EnterSegment(int newSegment)
    {
        PeopleReferences.GetWaypoints(newSegment, ref waypointA, ref waypointB);
        trackDistance = Vector3.Distance(waypointA.position, waypointB.position);
        trackProgress = 0;
    }

    private void Update()
    {
        FindState();
        ActOnState();
    }

    private void Start()
    {
        Ini();
    }
}
