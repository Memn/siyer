using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRotation3D : MonoBehaviour
{


    // register collision
    bool collisionOccurred;

    // public GameObject risingText;
    private SiyerBow bow;

    // the vars realize the fading out of the arrow when target is hit
    float alpha;
    float life_loss;
    public Color color = Color.white;

    // Use this for initialization
    void Start()
    {
        bow = GameObject.FindObjectOfType<SiyerBow>();
        // set the initialization values for fading out
        float duration = 5f;
        life_loss = 1f / duration;
        alpha = 1f;
    }



    // Update is called once per frame
    void Update()
    {
        //this part of update is only executed, if a rigidbody is present
        // the rigidbody is added when the arrow is shot (released from the bowstring)
        // print(transform.position);

        if (transform.GetComponent<Rigidbody>() != null)
        {

            // do we fly actually?
            if (GetComponent<Rigidbody>().velocity != Vector3.zero)
            {
                // get the actual velocity
                Vector3 vel = GetComponent<Rigidbody>().velocity;
                transform.rotation = Quaternion.LookRotation(vel);

                // calc the rotation from x and y velocity via a simple atan2
                // float angleZ = Mathf.Atan2(vel.y, vel.x) * Mathf.Rad2Deg;
                // float angleY = Mathf.Atan2(vel.z, vel.x) * Mathf.Rad2Deg;
                // rotate the arrow according to the trajectory
                //  transform.eulerAngles = new Vector3(0, 0, 0);
                //
            }
        }


        // if the arrow hit something...
        if (collisionOccurred)
        {
            // fade the arrow out
            alpha -= Time.deltaTime * life_loss;
            GetComponent<Renderer>().material.color = new Color(color.r, color.g, color.b, alpha);

            // if completely faded out, die:
            if (alpha <= 0f)
            {
                Die();
            }
        }
        if (transform.position.y < 0)
        {
            Die();
        }
    }

    private void Die()
    {
        bow.ShootCompleted(0);
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

        //so, did a collision occur already?
        if (collisionOccurred)
        {
            // fix the arrow and let it not move anymore
            transform.position = new Vector3(other.transform.position.x, transform.position.y, transform.position.z);
            // the rest of the method hasn't to be calculated
            return;
        }

        // I installed cubes as border collider outside the screen
        // If the arrow hits these objects, the player lost an arrow
        if (other.transform.name == "Plane")
        {
            Destroy(gameObject);
            bow.ShootCompleted(0);
        }

        // Ok - 
        // we hit the target
        if (other.transform.name == "target")
        {
            // set velocity to zero
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            // disable the rigidbody
            GetComponent<Rigidbody>().isKinematic = true;
            transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            // and a collision occurred
            collisionOccurred = true;

            Destroy(gameObject);
            bow.ShootCompleted(10);

        }
    }

}

