using System.Collections;
using UnityEngine;

public class ElvenArrow : MonoBehaviour
{
    // public GameObject risingText;
//    private ElvenBow bow;

    private TalimhaneManager _manager;

    // Use this for initialization
    private void Start()
    {
        Physics.gravity = Vector3.down * 6f;
        _manager = FindObjectOfType<TalimhaneManager>();
//        bow = FindObjectOfType<ElvenBow>();
    }


    // Update is called once per frame
    private void Update()
    {
        //this part of update is only executed, if a rigidbody is present
        // the rigidbody is added when the arrow is shot (released from the bowstring)
        // print(transform.position);

        // do we fly actually?
        if (GetComponent<Rigidbody>().velocity != Vector3.zero)
        {
            // get the actual velocity
            var vel = GetComponent<Rigidbody>().velocity;
            // transform.rotation = Quaternion.LookRotation(vel);
            transform.forward = Vector3.Slerp(transform.forward, vel.normalized, Time.deltaTime);
        }

        if (transform.position.y < -100)
        {
            Die("negative y");
        }
    }

    private void Die(string source)
    {
//        bow.ShootCompleted();
        FindObjectOfType<TalimhaneCameraController>().ToggleCameras(source);
        // and destroy the current one
        Destroy(gameObject);
    }

    public void Hit(bool target)
    {
        StartCoroutine(HitSmth(target));
    }

    private IEnumerator HitSmth(bool target)
    {
        // set velocity to zero
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        // disable the rigidbody
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        if (target)
        {
            _manager.Hit();
        }

        yield return new WaitForSecondsRealtime(2);
        // wait for a while 
        if (target)
        {
            FindObjectOfType<TalimhaneTarget>().SetPostion();
            Die("target");
        }
        else
        {
            _manager.NotHit();
            Die("plane");
        }
    }
}