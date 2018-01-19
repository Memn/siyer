using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class SiyerBow : MonoBehaviour
{

    // References to the gameobjects / prefabs
    GameObject arrow;
    public GameObject arrowPrefab;

    // public GameObject laserPrefab;

    private ArcheryMusicPlayer musicPlayer;

    bool stringReleased;
    bool stringPulled;


    public int arrows = 30;
    public int score = 0;

    public Text arrowValue;
    public Text scoreValue;
    public Text distanceValue;

    public enum ArcheryStatus { Ready, Pulled, Released, OutOfArrows };

    float power;
    public ArcheryStatus status;
    // Use this for initialization

    private SiyerSpriteAtlasHelper atlasHelper;

    void Start()
    {
        // create an arrow to shoot
        ChangeStatus(ArcheryStatus.Ready);
        UpdateBoard();
        atlasHelper = this.GetComponent<SiyerSpriteAtlasHelper>();
        musicPlayer = GameObject.FindObjectOfType<ArcheryMusicPlayer>();


    }

    // Update is called once per frame
    void Update()
    {
        UpdateBoard();
        switch (status)
        {
            case ArcheryStatus.Ready:
                atlasHelper.currentSprite = SiyerSpriteAtlasHelper.SpriteType.BowAndHandsReady;
                // when a new arrow is created means that:
                // sounds has been played
                stringReleased = false;
                stringPulled = false;

                break;
            case ArcheryStatus.Pulled:
                if (!stringPulled)
                {
                    atlasHelper.currentSprite = SiyerSpriteAtlasHelper.SpriteType.BowAndHandsPulled;
                    musicPlayer.Play(ArcheryMusicPlayer.AudioClips.StringPull);
                    stringPulled = true;
                }
                break;
            case ArcheryStatus.Released:
                if (!stringReleased)
                {
                    atlasHelper.currentSprite = SiyerSpriteAtlasHelper.SpriteType.BowAndHandsReleased;
                    musicPlayer.Play(ArcheryMusicPlayer.AudioClips.StringRelease);
                    // play arrow sound
                    musicPlayer.Play(ArcheryMusicPlayer.AudioClips.ArrowSwoosh);
                    stringReleased = true;
                    Shoot();
                }
                break;
            case ArcheryStatus.OutOfArrows:
                atlasHelper.currentSprite = SiyerSpriteAtlasHelper.SpriteType.BowAndHandsReleased;
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
            ChangeStatus(ArcheryStatus.Pulled);
        }

        // ok, player released the mouse
        // (player released the touch on android)
        if (Input.GetMouseButtonUp(0))
        {
            ChangeStatus(ArcheryStatus.Released);
        }
    }

    private void Shoot()
    {
        CreateArrow();
        // subtract one arrow
        arrows--;
        PrepareArrow();
        // shot the arrow (rigid body physics)                
        ShootArrow();
    }

    //
    // public void createArrow()
    //
    // this method creates a new arrow based on the prefab
    //
    public void CreateArrow()
    {

        // now instantiate a new arrow
        arrow = Instantiate(arrowPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        arrow.name = "arrow";
        arrow.transform.parent = transform;
        arrow.transform.localRotation = new Quaternion(0, 0, 0, 0);
        arrow.transform.localPosition = new Vector3(0, -8.0f, 0);
        arrow.transform.localScale = new Vector3(3, 3, 0.1f);

        FollowingCamera following = FindObjectOfType<FollowingCamera>();
        following.setTarget(arrow.transform);
    }


    public void UpdateBoard()
    {
        scoreValue.text = score.ToString();
        arrowValue.text = arrows.ToString();
        distanceValue.text = CalculateDistance();
    }

    private string CalculateDistance()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider)
            {
                float distance = Vector3.Distance(transform.position, hit.transform.position);
                return distance.ToString("F2") + "m";
            }

        }
        return " >1000m";
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

        if (Physics.Raycast(mouseRay1, out rayHit, 1000f))
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

    }

    //
    // public void shootArrow()
    //
    // Player released the arrow
    // get the bows rotationn and accelerate the arrow
    //
    public void ShootArrow()
    {
        arrow.GetComponent<Rigidbody>().velocity = arrow.transform.forward * 100;
        FindObjectOfType<CameraController>().ToggleCameras("shoot");
    }

    public void ShootCompleted()
    {
        ChangeStatus(ArcheryStatus.Ready);
    }

    public void ArrowHit()
    {
        musicPlayer.Play(ArcheryMusicPlayer.AudioClips.ArrowImpact);
        score += 10;
    }

    private void ChangeStatus(ArcheryStatus status)
    {
        if (arrows == 0)
        {
            this.status = ArcheryStatus.OutOfArrows;
        }
        else
        {
            this.status = status;
        }

    }

}




