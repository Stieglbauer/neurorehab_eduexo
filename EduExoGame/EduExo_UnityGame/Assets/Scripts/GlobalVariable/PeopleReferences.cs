using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleReferences : MonoBehaviour
{
    [SerializeField]
    private Transform[] waypoints;

    [SerializeField]
    private float queueDistance;

    private static PeopleReferences reference;
    public static PeopleReferences GetReference()
    {
        if(reference == null) { 
            reference = GameObject.FindObjectOfType<PeopleReferences>();
            if(reference == null)
            {
                throw new System.Exception("No original singleton of type 'PeopleReferences' found!");
            }
        }
        return reference;
    }

    // Initialize
    private void Awake()
    {
        reference = null;
    }

    public static float GetQueueDistance()
    {
        return GetReference().queueDistance;
    }

    public static void GetWaypoints(int segment, ref Transform waypointA, ref Transform waypointB)
    {
        waypointA = GetReference().waypoints[2 * segment];
        waypointB = GetReference().waypoints[2 * segment + 1];
    }
}
