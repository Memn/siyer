using System;
using UnityEngine;

public class TalimhaneMusicPlayer : MonoBehaviour
{
    // Sound effects
    public AudioClip StringPull;
    public AudioClip StringRelease;
    public AudioClip ArrowSwoosh;
    public AudioClip ArrowImpact;

    private AudioSource _audioSource;

    public enum AudioClips
    {
        StringPull,
        StringRelease,
        ArrowSwoosh,
        ArrowImpact
    }


    // Use this for initialization
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Play(AudioClips audio)
    {
        switch (audio)
        {
            case AudioClips.StringPull:
                _audioSource.PlayOneShot(StringPull);
                break;
            case AudioClips.StringRelease:
                _audioSource.PlayOneShot(StringRelease);
                break;
            case AudioClips.ArrowSwoosh:
                _audioSource.PlayOneShot(ArrowSwoosh);
                break;
            case AudioClips.ArrowImpact:
                _audioSource.PlayOneShot(ArrowImpact);
                break;
            default:
                throw new ArgumentOutOfRangeException("audio", audio, null);
        }
    }
}