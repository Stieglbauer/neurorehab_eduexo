using UnityEngine;

public class ArmBehaviour : MonoBehaviour
{
    [SerializeField]
    private float angle;

    [SerializeField]
    private bool grab;

    [SerializeField]
    private Renderer[] grabRenderer, nonGrabRenderer;

    [SerializeField]
    private Transform arm;

    public void SetGrab(bool grab)
    {
        this.grab = grab;

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
        this.angle = angle;
        this.arm.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 22));
    }
}
