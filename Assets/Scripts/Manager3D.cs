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
    public GameObject bowString;
    GameObject arrow;
    public GameObject gameManager;
    public GameObject arrowPrefab;

    public GameObject target;

    // Sound effects
    public AudioClip stringPull;
    public AudioClip stringRelease;
    public AudioClip arrowSwoosh;

    // has sound already be played
    bool stringPullSoundPlayed;
    bool stringReleaseSoundPlayed;
    bool arrowSwooshSoundPlayed;

    // the bowstring is a line renderer
    private List<Vector3> bowStringPosition;
    LineRenderer bowStringLinerenderer;

    // to determine the string pullout
    float arrowStartX;
    float length;

    // some status vars
    bool arrowShot;
    bool arrowPrepared;

    // position of the line renderers middle part 
    Vector3 stringPullout;
    Vector3 stringRestPosition = new Vector3(-0.62f, 0f, 0f);

    public int arrows = 20;

    // Use this for initialization
    void Start()
    {

        arrowStartX = 0.7f;
        stringPullout = stringRestPosition;
        // create an arrow to shoot
        createArrow(); 
        createBowString();
        drawBowString();
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
            arrow.transform.localPosition = this.transform.position;
            arrow.transform.localRotation = this.transform.localRotation;
            arrow.transform.parent = this.transform;
            // transmit a reference to the arrow script
            arrow.GetComponent<ArrowRotation3D>().bow = gameObject;
            arrowShot = false;
            arrowPrepared = false;
            // subtract one arrow
            arrows--;
        }

    }

    private void createBowString()
    {
        // setup the line renderer representing the bowstring
        bowStringLinerenderer = bowString.AddComponent<LineRenderer>();
        bowStringLinerenderer.positionCount = 3;
        bowStringLinerenderer.startWidth = 0.01F;
        bowStringLinerenderer.endWidth = 0.01F;
        bowStringLinerenderer.useWorldSpace = false;
        bowStringLinerenderer.material = Resources.Load("bowStringMaterial") as Material;
        bowStringPosition = new List<Vector3>();
        bowStringPosition.Add(new Vector3(-0.55f, 1.31f, 0f));
        bowStringPosition.Add(new Vector3(-0.62f, 0f, 0f));
        bowStringPosition.Add(new Vector3(-0.47f, -1.29f, 0f));
        bowStringLinerenderer.SetPosition(0, bowStringPosition[0]);
        bowStringLinerenderer.SetPosition(1, bowStringPosition[1]);
        bowStringLinerenderer.SetPosition(2, bowStringPosition[2]);

    }

    //
    // public void drawBowString()
    //
    // set the bowstrings line renderer position
    //

    public void drawBowString()
    {
        bowStringLinerenderer = bowString.GetComponent<LineRenderer>();
        bowStringLinerenderer.SetPosition(0, bowStringPosition[0]);
        bowStringLinerenderer.SetPosition(1, stringPullout);
        bowStringLinerenderer.SetPosition(2, bowStringPosition[2]);
    }

    // Update is called once per frame
    void Update()
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
            }
            // detrmine the pullout and set up the arrow
            prepareArrow();
        }

        // ok, player released the mouse
        // (player released the touch on android)
        if (Input.GetMouseButtonUp(0) && arrowPrepared)
        {
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
            // shot the arrow (rigid body physics)
            shootArrow();
        }
        // in any case: update the bowstring line renderer
        drawBowString();

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
            // set the bowstrings line renderer
            stringPullout = new Vector3(-(0.44f + length), -0.06f, 2f);
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
            //arrow.transform.parent = gameManager.transform;
            arrow.GetComponent<Rigidbody>().AddForce(
                Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x,
                                            transform.rotation.eulerAngles.y,
                                            transform.rotation.eulerAngles.z)) *
                                 new Vector3(25f * length, 0, 0), ForceMode.VelocityChange);
        }
        arrowPrepared = false;
        stringPullout = stringRestPosition;

        // Cam
        // Camera.main.GetComponent<camMovement2>().resetCamera();
        // Camera.main.GetComponent<camMovement2>().setArrow(arrow);

    }

}




