using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    float xMin = -12.0f;
    float xMax = 12.0f;
    float zMin = -270.0f; // -300-270 = 30 meters far
    float zMax = -150.0f; //  -300-150  = 150 meters far

    public void SetPostion()
    {
        transform.position = new Vector3(Random.Range(xMin, xMax), transform.position.y, Random.Range(zMin, zMax));
    }

}
