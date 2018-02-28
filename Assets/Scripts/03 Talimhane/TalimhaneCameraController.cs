using UnityEngine;

public class TalimhaneCameraController : MonoBehaviour
{
    public Camera fpc;
    public Camera following;
    public GameObject Aim;


    // Use this for initialization
    private void Start()
    {
        fpc.enabled = true;
        following.enabled = false;
    }

    public void ToggleCameras(string source)
    {
//        print("Toggled by " + source);
        fpc.enabled = !fpc.enabled;
        following.enabled = !following.enabled;
        Aim.GetComponent<SpriteRenderer>().enabled = !Aim.GetComponent<SpriteRenderer>().enabled;
    }
}