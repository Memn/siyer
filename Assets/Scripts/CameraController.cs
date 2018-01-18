using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Camera fpc;
    public Camera following;


    // Use this for initialization
    void Start()
    {
        fpc.enabled = true;
        following.enabled = false;
    }

   public void ToggleCameras(string source)
    {
        print("Toggled by "+ source);
        fpc.enabled = !fpc.enabled;
        following.enabled = !following.enabled;
    }
}
