using UnityEngine;

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

        if (!other.transform.CompareTag("Goal")) return;
        if (_collided) return;
        Debug.Log("Hit : " + other.transform.name);
        _collided = true;
        if (other.name.Equals("bind_r_bag01"))
        {
            transform.parent.transform.parent = other.transform;
            transform.parent.GetComponent<ElvenArrow>().RightHit();
        }

        if (other.name.Equals("bind_l_bag01"))
        {
            transform.parent.transform.parent = other.transform;
            transform.parent.GetComponent<ElvenArrow>().LeftHit();
        }

        if (other.name.Equals("bind_perekladina01"))
            transform.parent.GetComponent<ElvenArrow>().Hit(true);
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