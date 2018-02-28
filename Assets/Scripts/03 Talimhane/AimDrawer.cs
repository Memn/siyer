using UnityEngine;

public class AimDrawer : MonoBehaviour
{
    public GameObject Aim;

    private void Update()
    {
//        var transformDirection = transform.TransformDirection(Vector3.forward) * 300;
        var transformPoint = transform.TransformPoint(Vector3.forward * 600);
        Aim.transform.localScale = Vector3.one * 0.5f;
        Aim.transform.position = transformPoint;
    }
}