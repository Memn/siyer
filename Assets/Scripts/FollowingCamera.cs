using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{

    private Transform target;

    public Vector3 offset;

    public float smoothSpeed = 0.125f;


    // Update is called once per frame
    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
        }
    }

    public void setTarget(Transform transform)
    {
        target = transform;
    }
}
