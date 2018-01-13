using UnityEngine;

public class CameraFollow : MonoBehaviour
{

	public Vector3 offset;

    private Camera cam;

    void Start()
    {
        cam = this.GetComponent<Camera>();
    }
    void LateUpdate()
    {
        cam.transform.localPosition = offset;
    }
}
