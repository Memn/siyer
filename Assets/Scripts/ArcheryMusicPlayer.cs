using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcheryMusicPlayer : MonoBehaviour
{

    // Sound effects
    public AudioClip stringPull;
    public AudioClip stringRelease;
    public AudioClip arrowSwoosh;
    public AudioClip arrowImpact;

    private AudioSource audioSource;

    public enum AudioClips { StringPull, StringRelease, ArrowSwoosh, ArrowImpact }


    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Play(AudioClips audio)
    {
        switch (audio)
        {
            case AudioClips.StringPull:
                audioSource.PlayOneShot(stringPull);
                break;
            case AudioClips.StringRelease:
                audioSource.PlayOneShot(stringRelease);
                break;
            case AudioClips.ArrowSwoosh:
                audioSource.PlayOneShot(arrowSwoosh);
                break;
            case AudioClips.ArrowImpact:
                audioSource.PlayOneShot(arrowImpact);
                break;

            default:
                break;
        }
    }
}
