using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleReferences : MonoBehaviour
{
    [SerializeField]
    private Transform[] waypoints;
    
    private List<List<EnterCondition>> enterConditions = new List<List<EnterCondition>>();

    [SerializeField]
    private float queueDistance;

    [SerializeField]
    private ArmBehaviour arm;
    
    private List<List<PeopleBehaviour>> queues;

    public const int armSegment = 1;

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

    private int GetSegmentCount()
    {
        return waypoints.Length / 2;
    }

    // Initialize
    private void Awake()
    {
        reference = null;
        queues = new List<List<PeopleBehaviour>>();
        for(int i=0; i< GetSegmentCount(); i++)
        {
            queues.Add(new List<PeopleBehaviour>());
        }
        while (enterConditions.Count < GetSegmentCount())
        {
            enterConditions.Add(new List<EnterCondition>());
        }
    }

    public static Vector3 GetQueuePosition(int trackSegment, int queuePosition)
    {
        Transform waypointA = null, waypointB = null;
        GetWaypoints(trackSegment, ref waypointA, ref waypointB);

        return UTIL.LerpAbsDistance(waypointB.position, waypointA.position, queuePosition * GetReference().queueDistance);
    }

    /// <summary>
    /// Gets the two waypoints defining the next segment. Returns true, if no further segment is available.
    /// </summary>
    public static bool GetWaypoints(int trackSegment, ref Transform waypointA, ref Transform waypointB)
    {
        var waypoints = GetReference().waypoints;
        if (2 * trackSegment < waypoints.Length)
        {
            waypointA = waypoints[2 * trackSegment];
            waypointB = waypoints[2 * trackSegment + 1];
            return false;
        } else
        {
            waypointA = waypoints[0];
            waypointB = waypoints[1];
            return true;
        }
    }

    public static int GetQueueIndex(int trackSegment, PeopleBehaviour people)
    {
        var queue = GetReference().queues[trackSegment];
        if (!queue.Contains(people))
        {
            queue.Add(people);
            HandleSegmentSwitch();
        }

        return queue.FindIndex(p => p == people);
    }

    public static bool RequestQueueEnd(int nextTrackSegment, PeopleBehaviour people)
    {
        var reference = GetReference();
        bool requestGranted = false;
        if (EnterCondition.EvalAll(reference.enterConditions[nextTrackSegment - 1])) {
            if (nextTrackSegment == reference.GetSegmentCount())
            {
                // always allow to leave last segment
                requestGranted = true;
            } else if (reference.queues[nextTrackSegment].Count == 0)
            {
                // enter next one if next segment has no people queueing
                requestGranted = true;
            } else
            {
                // only allow to leave segment if the next one is not full
                Transform waypointA = null, waypointB = null;
                GetWaypoints(nextTrackSegment, ref waypointA, ref waypointB);

                requestGranted = Vector3.Distance(waypointA.position, waypointB.position) >= (reference.queues[nextTrackSegment].Count) * reference.queueDistance;
            }
        }

        if(requestGranted)
        {
            reference.queues[nextTrackSegment-1].RemoveAll(p => p == people);
        }

        return requestGranted;
    }

    public static void AddEnterCondition(EnterCondition condition, int trackSegment)
    {
        GetReference().enterConditions[trackSegment].Add(condition);
    }

    public static void HandleSegmentSwitch()
    {
        var reference = GetReference();
        reference.arm.SetGrab(reference.queues[armSegment].Count > 0);
    }
}
