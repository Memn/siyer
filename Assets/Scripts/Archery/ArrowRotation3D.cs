using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRotation3D : MonoBehaviour
{


    // register collision
    bool collisionOccurred;

    // References to GameObjects gset in the inspector
    public GameObject arrowHead;
    // public GameObject risingText;
    public GameObject bow;

    // Reference to audioclip when target is hit
    public AudioClip targetHit;

    // the vars realize the fading out of the arrow when target is hit
    float alpha;
    float life_loss;
    public Color color = Color.white;

    // Use this for initialization
    void Start()
    {
        // set the initialization values for fading out
        float duration = 2f;
        life_loss = 1f / duration;
        alpha = 1f;
    }



    // Update is called once per frame
    void Update()
    {
        //this part of update is only executed, if a rigidbody is present
        // the rigidbody is added when the arrow is shot (released from the bowstring)
        if (transform.GetComponent<Rigidbody>() != null)
        {
            // do we fly actually?
            if (GetComponent<Rigidbody>().velocity != Vector3.zero)
            {
                // get the actual velocity
                Vector3 vel = GetComponent<Rigidbody>().velocity;
                // calc the rotation from x and y velocity via a simple atan2
                float angleZ = Mathf.Atan2(vel.y, vel.x) * Mathf.Rad2Deg;
                float angleY = Mathf.Atan2(vel.z, vel.x) * Mathf.Rad2Deg;
                // rotate the arrow according to the trajectory
                transform.eulerAngles = new Vector3(0, -angleY, angleZ);
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
                // create new arrow
                bow.GetComponent<Manager3D>().changeStatus(Manager3D.ArchingStatus.Ready);

                // and destroy the current one
                Destroy(gameObject);
            }
        }
    }


    //
    // void OnCollisionEnter(Collision other)
    //
    // other: the other object the arrow collided with
    //


    void OnCollisionEnter(Collision other)
    {

        // // we have to determine a score
        int actScore = 0;

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
            bow.GetComponent<Manager3D>().changeStatus(Manager3D.ArchingStatus.Ready);
            Destroy(gameObject);
        }

        // Ok - 
        // we hit the target
        if (other.transform.name == "target")
        {
            // play the audio file ("trrrrr")
            GetComponent<AudioSource>().PlayOneShot(targetHit);
            // set velocity to zero
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            // disable the rigidbody
            GetComponent<Rigidbody>().isKinematic = true;
            transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            // and a collision occurred
            collisionOccurred = true;
            // disable the arrow head to create optical illusion
            // that arrow hit the target
            arrowHead.SetActive(false);
            // though there may be more than one contact point, we take 
            // the first one in order
            // we must determine where the other object has been hit
            // float x;
            // float y;
            // float z;
            // y = other.contacts[0].point.y;
            // x = other.contacts[0].point.x;
            // z = other.contacts[0].point.z;
            // y is the absolute coordinate on the screen, not on the collider, 
            // so we subtract the center's position
            Vector3 center = new Vector3(0.2f, 0.3f, 0);
            Vector3 contact = other.contacts[0].point - other.transform.position;
            //            Vector3 projected = Vector3.Project(contact, new Vector3(0.17f, -0.2f, 0));
            // Vector3 projected = Vector3.Project(contact, -new Vector3(contact.x, contact.y, 0));
            float distance = Vector3.Distance(new Vector3(contact.x, contact.y, 0), center);
            // Quaternion rot = Quaternion.FromToRotation(Vector3.up, other.contacts[0].normal);
            // Instantiate(explosionPrefab, other.contacts[0].point, rot);


            // x total 0.75
            // y total 3.5
            // we hit at least white...
            if (distance > 0.64)
                actScore = 10;
            // ... it could be black, too ...
            if (distance <= 0.64 && distance > 0.44)
                actScore = 20;
            // ... even blue is possible ...
            if (distance <= 0.44 && distance > 0.31)
                actScore = 30;
            // ... or red ...
            if (distance <= 0.31 && distance > 0.19)
                actScore = 40;
            // ... or gold !!!
            if (distance <= 0.19)
                actScore = 50;

            // GameObject rt = (GameObject)Instantiate(risingText, new Vector3(0, 0, 0), Quaternion.identity);
            // rt.transform.position = other.transform.position + new Vector3(-1, 1, 0);
            // rt.transform.name = "rt";
            // rt.GetComponent<TextMesh>().text = "+" + actScore;
            // inform the master script about the score
            bow.GetComponent<Manager3D>().setPoints(actScore);
        }
    }

}

