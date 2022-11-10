using UnityEngine;

public class ArmBehaviour : MonoBehaviour
{
    [SerializeField]
    private Renderer[] grabRenderer, nonGrabRenderer;

    [SerializeField]
    private Transform arm;

    public void SetGrab(bool grab)
    {
        foreach(var renderer in grabRenderer)
        {
            renderer.enabled = grab;
        }
        foreach (var renderer in nonGrabRenderer)
        {
            renderer.enabled = !grab;
        }
    }

    public void SetAngle(float angle)
    {
        this.arm.localRotation = Quaternion.Euler(new Vector3(0, 0, angle + 20));
    }
}
