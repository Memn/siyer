using UnityEngine;

public class LabirentPlayer : MonoBehaviour
{
    public AudioClip HitSound;
    public AudioClip CoinSound;

    public MazeManager Manager;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }


    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag.Equals("Floor"))
        {
            if (_audioSource != null && HitSound != null && coll.relativeVelocity.y > .5f)
            {
                _audioSource.PlayOneShot(HitSound, coll.relativeVelocity.magnitude);
            }
        }
        else
        {
            if (_audioSource != null && HitSound != null && coll.relativeVelocity.magnitude > 2f)
            {
                _audioSource.PlayOneShot(HitSound, coll.relativeVelocity.magnitude);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.tag.Equals("Goal")) return;
        if (_audioSource != null && CoinSound != null)
        {
            _audioSource.PlayOneShot(CoinSound);
        }

        Destroy(other.gameObject);
        Manager.CoinCollected();
    }
}