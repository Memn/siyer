using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dede : MonoBehaviour
{


    private AudioSource source;

    public int sceneNumber = 0;

    public AudioClip[] clips;
    private Animator animator;
    public FilVakasiManager manager;

    void Start()
    {
        source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    public void SceneEnter()
    {
        sceneNumber = animator.GetInteger("scene number");
        EnterScene();
    }

    void EnterScene()
    {
        switch (sceneNumber)
        {
            case 1:
                Audio();
                Speak();
                Invoke("SceneExit", 10);
                break;
            case 2:
                Audio();
                Speak();
                Invoke("SceneExit", 9);
                break;
            case 3:
                Audio();
                Speak();
                Invoke("ShowingForward", 2.4f);
                Invoke("ShowingBackward", 4);
                Invoke("SceneExit", 9);
                break;
            default:
                throw new NotImplementedException("Scene number for Ebrehe is not valid: " + sceneNumber);
        }
    }

    private void Audio()
    {
        source.PlayOneShot(clips[sceneNumber - 1]);
    }
    void Speak()
    {
        animator.SetTrigger("speak");
    }
    void ShowingForward()
    {
        animator.SetTrigger("show-forward");
    }
    void ShowingBackward()
    {
        animator.SetTrigger("show-backward");
    }
    void SceneExit()
    {
        animator.SetTrigger("scene exit");
        manager.DedeExit();
    }
}
