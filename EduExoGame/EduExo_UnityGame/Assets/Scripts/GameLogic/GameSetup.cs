using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
    [SerializeField]
    private Transform arm, handRef;

    [SerializeField]
    private ArmBehaviour armBehaviour;

    [SerializeField]
    private ElbowAngle elbowAngle;

    [SerializeField]
    private Transform scene, cam;

    [SerializeField]
    private Transform leftSegment, rightSegment, leftSegA, leftSegB, rightSegA, rightSegB;

    private void Awake()
    {
        arm.rotation = Quaternion.Euler(0, 0, TechnicalData.upperArmAngle);
        MirrorScene(TechnicalData.leftArm);
    }

    private void MirrorScene(bool mirror)
    {
        scene.transform.localScale = new Vector3(1, 1, mirror ? 1 : -1);
        cam.transform.rotation = Quaternion.Euler(0, mirror ? 0 : 180, 0);
    }

    public void PositionSegment(Transform segment, Transform reference, Vector3 targetPosition)
    {
        Vector3 offset = segment.position - reference.position;

        segment.position = targetPosition + offset;
    }

    public void SetupSegment(bool left)
    {
        PositionSegment(left ? leftSegment : rightSegment, left ? leftSegB : rightSegA, handRef.position);
    }

    public void SetupSegment(bool left, float angle)
    {
        armBehaviour.SetAngle(angle);
        SetupSegment(left);
        armBehaviour.SetAngle(elbowAngle.elbowangle);
    }
}
