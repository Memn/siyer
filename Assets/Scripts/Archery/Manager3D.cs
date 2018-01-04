using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager3D : MonoBehaviour
{

    // References to the gameobjects / prefabs
    GameObject arrow;
    public GameObject arrowPrefab;

    // Sound effects
    public AudioClip stringPull;
    public AudioClip stringRelease;
    public AudioClip arrowSwoosh;

    // has sound already be played
    bool stringPullSoundPlayed;
    bool stringReleaseSoundPlayed;
    bool arrowSwooshSoundPlayed;

    // some status vars
    bool arrowShot;
    bool arrowPrepared;
    bool arrowCreated = false;

    public int arrows = 30;
    public int score = 0;

    public Text arrowValue;
    public Text scoreValue;

    public enum ArchingStatus { Ready, Pulled, Released };

    float power;
    public ArchingStatus status;
    // Use this for initialization
    bool debug = true;
    void Start()
    {

        // create an arrow to shoot
        ChangeStatus(ArchingStatus.Ready);
        UpdateBoard();

    }

    // Update is called once per frame
    void Update()
    {
        switch (status)
        {
            case ArchingStatus.Ready:
                if (!arrowCreated)
                {
                    if (arrows > 0)
                    {
                        this.GetComponent<Archery>().currentSprite = Archery.SpriteType.BowAndHandsReady;
                        CreateArrow();
                        arrowShot = false;
                        arrowPrepared = false;
                        arrowCreated = true;
                        // subtract one arrow
                        arrows--;
                        // transmit a reference to the arrow script
                        arrow.GetComponent<ArrowRotation3D>().bow = gameObject;
                    }
                    else
                    {
                        ChangeStatus(ArchingStatus.Released);
                    }
                }
                UpdateBoard();
                break;
            case ArchingStatus.Pulled:
                this.GetComponent<Archery>().currentSprite = Archery.SpriteType.BowAndHandsPulled;
                // detrmine the pullout and set up the arrow
                PrepareArrow();
                arrowPrepared = true;
                break;
            case ArchingStatus.Released:
                this.GetComponent<Archery>().currentSprite = Archery.SpriteType.BowAndHandsReleased;
                // shot the arrow (rigid body physics)
                if (arrowPrepared)
                {
                    arrow.SetActive(true);
                    ShootArrow();
                }

                break;
            default:
                break;
        }
        ListenMouseInput();

    }
    private void ListenMouseInput()
    {
        // game is steered via mouse
        // (also works with touch on android)
        if (Input.GetMouseButton(0))
        {
            // the player pulls the string
            if (!stringPullSoundPlayed)
            {
                // play sound
                GetComponent<AudioSource>().PlayOneShot(stringPull);
                stringPullSoundPlayed = true;
                ChangeStatus(ArchingStatus.Pulled);
            }
        }

        // ok, player released the mouse
        // (player released the touch on android)
        if (Input.GetMouseButtonUp(0) && arrowPrepared)
        {
            ChangeStatus(ArchingStatus.Released);
            // play string released sound
            if (!stringReleaseSoundPlayed)
            {
                GetComponent<AudioSource>().PlayOneShot(stringRelease);
                stringReleaseSoundPlayed = true;
            }
            // play arrow sound
            if (!arrowSwooshSoundPlayed)
            {
                GetComponent<AudioSource>().PlayOneShot(arrowSwoosh);
                arrowSwooshSoundPlayed = true;
            }
        }
    }

    //
    // public void createArrow()
    //
    // this method creates a new arrow based on the prefab
    //

    public void CreateArrow()
    {
        // when a new arrow is created means that:
        // sounds has been played
        stringPullSoundPlayed = false;
        stringReleaseSoundPlayed = false;
        arrowSwooshSoundPlayed = false;

        // now instantiate a new arrow
        arrow = Instantiate(arrowPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        arrow.name = "arrow";
        arrow.transform.parent = transform;
        arrow.transform.localRotation = new Quaternion(0, 0, 0, 0);
        arrow.transform.localPosition = new Vector3(transform.position.x, transform.position.y - 8.0f, 0);
        arrow.transform.localScale = new Vector3(3, 3, 0.1f);
        arrow.SetActive(debug);
    }


    public void UpdateBoard()
    {
        scoreValue.text = score.ToString();
        arrowValue.text = arrows.ToString();
    }

    // 
    // public void prepareArrow()
    //
    // Player pulls out the string
    //
    public void PrepareArrow()
    {
        // get the touch point on the screen
        RaycastHit rayHit;
        // to determine the mouse position, we need a raycast
        Ray mouseRay1 = Camera.main.ScreenPointToRay(Input.mousePosition);
        // if (Physics.Raycast(transform.position, transform.forward, out hit))

        if (Physics.Raycast(mouseRay1, out rayHit, 1000f) && arrowShot == false)
        {

            // position of the raycast on the screen
            // determine the position on the screen
            float posX = rayHit.point.x;
            float posY = rayHit.point.y;
            float posZ = rayHit.point.z;
            // set the bows angle to the arrow
            Vector3 mousePulledDifference = new Vector3(transform.position.x - posX,
                        transform.position.y - posY,
                        transform.position.z - posZ);
            // float angleZ = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            // determine the arrow pullout
            power = mousePulledDifference.magnitude / 3f;
            power = Mathf.Clamp(power, 20, 100);
        }
        arrowPrepared = true;
    }

    //
    // public void shootArrow()
    //
    // Player released the arrow
    // get the bows rotationn and accelerate the arrow
    //

    public void ShootArrow()
    {
        if (arrow.GetComponent<Rigidbody>() == null)
        {
            arrowShot = true;
            arrow.AddComponent<Rigidbody>();
            // arrow.GetComponent<Rigidbody>().AddForce(Quaternion.Euler(transform.rotation.eulerAngles) *
            //                      new Vector3(3, 0, 0), ForceMode.VelocityChange);
            // arrow.GetComponent<Camera>().enabled = true;

            //arrow.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * 30);
            arrow.GetComponent<Rigidbody>().velocity = Camera.main.transform.forward * 30;

        }
        arrowPrepared = false;
        arrowCreated = false;

        // Cam
        // Camera.main.GetComponent<camMovement2>().resetCamera();
        // Camera.main.GetComponent<camMovement2>().setArrow(arrow);

    }

    public void SetPoints(int points)
    {
        score += points;
        // if (points == 50) {
        // 	arrows++;
        // 	GameObject rt1 = (GameObject)Instantiate(risingText, new Vector3(0,0,0),Quaternion.identity);
        // 	rt1.transform.position = this.transform.position + new Vector3(0,0,0);
        // 	rt1.transform.name = "rt1";
        // 	// each target's "ring" is 0.07f wide
        // 	// so it's relatively simple to calculate the ring hit (thus the score)
        // 	rt1.GetComponent<TextMesh>().text= "Bonus arrow";
        // }
    }

    public void ChangeStatus(ArchingStatus status)
    {
        if (arrows == 0)
        {
            this.status = ArchingStatus.Released;
        }
        else
        {
            this.status = status;
        }

    }

}




