﻿using UnityEngine;

public class ElvenArrowBody : MonoBehaviour
{
    // public GameObject risingText;
//    private ElvenBow bow;

    private bool _collided;

    // Use this for initialization
    private void Start()
    {
        _collided = false;
//        bow = FindObjectOfType<ElvenBow>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // I installed cubes as border collider outside the screen
        // If the arrow hits these objects, the player lost an arrow
        if (other.transform.name == "Plane")
        {
            if (!_collided)
            {
                _collided = true;
//                Debug.Log("Hit Plane!");
                transform.parent.GetComponent<ElvenArrow>().Hit(false);
            }
        }

        // Ok - 
        // we hit the target
        if (other.transform.name == "target")
        {
            if (!_collided)
            {
//                Debug.Log("Hit Target!");
                _collided = true;
                transform.parent.GetComponent<ElvenArrow>().Hit(true);
            }
        }
    }


    //
    // void OnCollisionEnter(Collision other)
    //
    // other: the other object the arrow collided with
    //
    private void OnCollisionEnter(Collision other)
    {
        // I installed cubes as border collider outside the screen
        // If the arrow hits these objects, the player lost an arrow
        if (other.transform.name == "Plane")
        {
            if (!_collided)
            {
                _collided = true;
//                Debug.Log("Hit Plane!");
                transform.parent.GetComponent<ElvenArrow>().Hit(false);
            }
        }

        // Ok - 
        // we hit the target
        if (other.transform.name == "target")
        {
            if (!_collided)
            {
//                Debug.Log("Hit Target!");
                _collided = true;
                transform.parent.GetComponent<ElvenArrow>().Hit(true);
            }
        }
    }
}