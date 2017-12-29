using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager3D : MonoBehaviour
{
    // to determine the mouse position, we need a raycast
    private Ray mouseRay1;
    private RaycastHit rayHit;
    // position of the raycast on the screen
    private float posX;
    private float posY;
    private float posZ;

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

    float length;

    // some status vars
    bool arrowShot;
    bool arrowPrepared;
    bool arrowCreated = false;

    public int arrows = 10;
    public int score = 0;

    public Text arrowValue;
    public Text scoreValue;

    private LineRenderer laser;

    public enum ArchingStatus { Ready, Pulled, Released };

    public ArchingStatus status;
    // Use this for initialization
    void Start()
    {

        laser = new LineRenderer();

        // create an arrow to shoot
        changeStatus(ArchingStatus.Ready);
        showArrows();
        showScore();

    }

    //
    // public void createArrow()
    //
    // this method creates a new arrow based on the prefab
    //

    public void createArrow()
    {
        // Camera.main.GetComponent<camMovement2>().resetCamera();
        // when a new arrow is created means that:
        // sounds has been played
        stringPullSoundPlayed = false;
        stringReleaseSoundPlayed = false;
        arrowSwooshSoundPlayed = false;
        // does the player has an arrow left ?
        if (arrows > 0)
        {


            // now instantiate a new arrow
            //this.transform.localRotation = Quaternion.identity;
            arrow = Instantiate(arrowPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            arrow.name = "arrow";
            arrow.transform.localScale = this.transform.localScale;
            //            arrow.transform.localRotation = this.transform.localRotation;
            // arrow.transform.localPosition = this.transform.position;
            // transmit a reference to the arrow script
            arrow.GetComponent<ArrowRotation3D>().bow = gameObject;
            arrowShot = false;
            arrowPrepared = false;
            arrowCreated = true;
            // subtract one arrow
            arrows--;
        }
        else
        {
            changeStatus(ArchingStatus.Released);
        }

    }

    // Update is called once per frame
    void Update()
    {

        switch (status)
        {
            case ArchingStatus.Ready:
                this.GetComponent<Archery>().currentSprite = Archery.SpriteType.BowAndHandsReady;
                if (!arrowCreated)
                {
                    createArrow();
                }
                arrow.transform.localRotation = new Quaternion(0, 0, 0, 0);
                arrow.transform.parent = this.transform;
                arrow.transform.localScale = Vector2.one;
                arrow.transform.localPosition = new Vector3(-0.2f, -7.0f, 0);
                arrow.SetActive(true);
                break;
            case ArchingStatus.Pulled:
                this.GetComponent<Archery>().currentSprite = Archery.SpriteType.BowAndHandsPulled;
                // detrmine the pullout and set up the arrow
                arrow.transform.localPosition = new Vector3(this.transform.position.x - 0.8f, this.transform.position.y - 9.0f, 0);
                // prepareArrow();
                arrowPrepared = true;
                break;
            case ArchingStatus.Released:
                this.GetComponent<Archery>().currentSprite = Archery.SpriteType.BowAndHandsReleased;
                // shot the arrow (rigid body physics)
                arrow.SetActive(true);
                shootArrow();
                showArrows();
                showScore();
                break;
            default:
                break;
        }
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
                changeStatus(ArchingStatus.Pulled);
            }
        }

        // ok, player released the mouse
        // (player released the touch on android)
        if (Input.GetMouseButtonUp(0) && arrowPrepared)
        {
            changeStatus(ArchingStatus.Released);
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

    private void showScore()
    {
        scoreValue.text = score.ToString();
    }

    private void showArrows()
    {
        arrowValue.text = arrows.ToString();
    }

    // 
    // public void prepareArrow()
    //
    // Player pulls out the string
    //

    public void prepareArrow()
    {
        // get the touch point on the screen
        mouseRay1 = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(mouseRay1, out rayHit, 1000f) && arrowShot == false)
        {
            // determine the position on the screen
            posX = this.rayHit.point.x;
            posY = this.rayHit.point.y;
            posZ = this.rayHit.point.z;
            // set the bows angle to the arrow
            Vector3 mousePos = new Vector3(transform.position.x - posX,
                        transform.position.y - posY,
                        transform.position.z - posZ);
            float angleZ = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            float angleY = Mathf.Atan2(mousePos.x, mousePos.z) * Mathf.Rad2Deg * 2;
            transform.eulerAngles = new Vector3(0, angleY, 0);
            // determine the arrow pullout
            length = mousePos.magnitude / 3f;
            length = Mathf.Clamp(length, 1, 1);
            arrow.transform.localPosition = new Vector3(this.transform.position.x - 1.9f, this.transform.position.y - 7.0f, 0);

            // set the arrows position
            // Vector3 arrowPosition = arrow.transform.localPosition;
            //arrowPosition.x = (arrowStartX - length);

            //arrow.transform.localPosition = arrowPosition;
        }
        arrowPrepared = true;
    }

    //
    // public void shootArrow()
    //
    // Player released the arrow
    // get the bows rotationn and accelerate the arrow
    //

    public void shootArrow()
    {
        if (arrow.GetComponent<Rigidbody>() == null)
        {
            arrowShot = true;
            arrow.AddComponent<Rigidbody>();

            arrow.GetComponent<Rigidbody>().AddForce(Quaternion.Euler(transform.rotation.eulerAngles) *
                                 new Vector3(0, 3, 0), ForceMode.VelocityChange);
        }
        arrowPrepared = false;
        arrowCreated = false;

        // Cam
        // Camera.main.GetComponent<camMovement2>().resetCamera();
        // Camera.main.GetComponent<camMovement2>().setArrow(arrow);

    }

    public void setPoints(int points)
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

    public void changeStatus(ArchingStatus status)
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




