using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilAnimationController : MonoBehaviour
{

    private int scene = 1;
    private AudioSource source;
    public AudioClip[] ebreheClips;

    public AudioClip[] dedeClips;
    void Start()
    {
        source = GetComponent<AudioSource>();
    }
    void Update()
    {
        // idle movements can be done here?
    }

    void EbrehePlay(int index)
    {
        source.PlayOneShot(ebreheClips[index]);
    }



}
