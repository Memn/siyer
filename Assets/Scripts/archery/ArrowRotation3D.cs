using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRotation3D : MonoBehaviour
{

    // public GameObject risingText;
    private SiyerBow bow;

    bool collided;

    // Use this for initialization
    void Start()
    {
        collided = false;
        bow = GameObject.FindObjectOfType<SiyerBow>();
    }



    // Update is called once per frame
    void Update()
    {
        //this part of update is only executed, if a rigidbody is present
        // the rigidbody is added when the arrow is shot (released from the bowstring)
        // print(transform.position);

        // do we fly actually?
        if (GetComponent<Rigidbody>().velocity != Vector3.zero)
        {
            // get the actual velocity
            Vector3 vel = GetComponent<Rigidbody>().velocity;
            transform.rotation = Quaternion.LookRotation(vel);
        }

        if (transform.position.y < 0)
        {
            Die("negative y");
        }

    }

    private void Die(string source)
    {
        bow.ShootCompleted();
        FindObjectOfType<CameraController>().ToggleCameras(source);
        // and destroy the current one
        Destroy(gameObject);
    }


    //
    // void OnCollisionEnter(Collision other)
    //
    // other: the other object the arrow collided with
    //
    void OnCollisionEnter(Collision other)
    {
        // I installed cubes as border collider outside the screen
        // If the arrow hits these objects, the player lost an arrow
        if (other.transform.name == "Plane")
        {
            if (!collided)
            {
                collided = true;
                Die("plane");
            }
        }

        // Ok - 
        // we hit the target
        if (other.transform.name == "target")
        {
            if (!collided)
            {
                collided = true;
                StartCoroutine(HitTarget());
            }
        }
    }

    IEnumerator HitTarget()
    {
        // set velocity to zero
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        // disable the rigidbody
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        bow.ArrowHit();
        yield return new WaitForSecondsRealtime(2);
        // wait for a while 
        FindObjectOfType<Target>().SetPostion();
        Die("target");
    }


}

