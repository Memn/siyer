using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ebrehe : MonoBehaviour
{

    private AudioSource source;

    private int sceneNumber = 0;
    private Animator animator;
    public AudioClip[] clips;

    public FilVakasiManager manager;

    void Start()
    {
        source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }


    void SceneEnter()
    {
        sceneNumber = animator.GetInteger("scene number");
        EnterScene(sceneNumber);
    }

    void EnterScene(int scene)
    {
        switch (scene)
        {
            case 1:
                ShowingDede();
                Audio(scene);
                Invoke("ShowingKaaba", 3.6f);
                Invoke("Angry", 7);
                Invoke("Laugh", 13.75f);
                Invoke("SceneExit", 17);
                break;
            case 2:
                Audio(scene);
                Laugh();
                Invoke("Mocking", 0.7f);
                Invoke("Angry", 5);
                Invoke("SceneExit", 15.5f);
                break;
            case 3:
                Angry();
                Invoke("ShowingKaaba", 0.5f);
                Audio(scene);
                Invoke("Speak", 5.5f);
                Invoke("SceneExit", 6.0f);
                break;
            default:
                throw new NotImplementedException("Scene number for Ebrehe is not valid: " + scene);
        }
    }

    private void Audio(int scene)
    {
        source.PlayOneShot(clips[scene - 1]);
    }

    void ShowingDede()
    {
        animator.SetTrigger("showing-dede");
    }
    void ShowingKaaba()
    {
        animator.SetTrigger("showing-kaaba");
    }

    void Speak()
    {
        animator.SetTrigger("speak");
    }
    void Angry()
    {
        animator.SetTrigger("angry");
    }

    void Laugh()
    {
        animator.SetTrigger("laugh");
    }
    void Mocking()
    {
        animator.SetTrigger("mocking");
    }
    void SceneExit()
    {
        animator.SetTrigger("scene exit");
        manager.EbreheExit();
    }

}
