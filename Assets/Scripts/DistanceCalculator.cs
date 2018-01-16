using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceCalculator : MonoBehaviour
{


    public Text distanceValue;
    private float distance = 1000f;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider)
            {
                distance = Vector3.Distance(transform.position, hit.transform.position);
                distanceValue.text = distance.ToString("F2") + "m";
            }

        }
        else
        {
            distanceValue.text = " >1000m";
        }

    }
}
