using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnterCondition : MonoBehaviour
{
    [SerializeField]
    protected int trackSegment;

    // Returns true if the enter condition is met
    public abstract bool Eval();

    public static bool EvalAll(ICollection<EnterCondition> conditions)
    {
        bool result = true;

        foreach(var condition in conditions)
        {
            result &= condition.Eval();
        }

        return result;
    }

    private void Start()
    {
        PeopleReferences.AddEnterCondition(this, trackSegment);
    }
}
