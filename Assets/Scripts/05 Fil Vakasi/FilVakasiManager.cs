using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FilVakasiManager : MonoBehaviour
{

    public Animator dede;
    public Animator ebrehe;

    private int scene = 1;
    private AudioSource source;

    public AudioClip[] ebreheClips;

    public AudioClip[] dedeClips;
    void Start()
    {
        source = GetComponent<AudioSource>();
        ebrehe.Play("idle");
        dede.Play("dede idle");
        StartScene();

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("02 Siyer");
        }
    }

    void DelayedAction(float delay, Action act)
    {
        StartCoroutine(PlayAnimationAfter(delay, act));
    }
    // The delay coroutine
    IEnumerator PlayAnimationAfter(float delay, Action act)
    {
        yield return new WaitForSeconds(delay);
        act();
    }


    void StartScene()
    {
        DelayedAction(1.5f, () =>
            {
                ebrehe.SetBool("speaking", true);
                source.PlayOneShot(ebreheClips[0]);
                DelayedAction(14.5f, () =>
                {
                    ebrehe.Play("laughing");
                    ebrehe.SetBool("speaking", false);
                });
            });

        // Dede Speaks for a while
        DelayedAction(20f, () =>
            {
                dede.SetBool("speaking", true);
                source.PlayOneShot(dedeClips[0]);
                DelayedAction(10f, () =>
                {
                    dede.SetBool("speaking", false);
                });
            });

        DelayedAction(30f, () =>
            {
                source.PlayOneShot(ebreheClips[1]);
                ebrehe.Play("laughing", -1, 0.5f);
                ebrehe.SetBool("speaking", true);
                DelayedAction(3, () =>
                {
                    ebrehe.SetBool("mocking", true);
                    DelayedAction(4, () =>
                    {
                        ebrehe.SetBool("mocking", false);
                        ebrehe.SetBool("angry", true);
                        DelayedAction(9.5f, () =>
                        {
                            ebrehe.SetBool("angry", false);
                            ebrehe.SetBool("speaking", false);
                        });
                    });
                });
                DelayedAction(15f, () =>
                {
                    ebrehe.SetBool("speaking", false);
                });
            });

        // Dede Speaks for a while
        DelayedAction(46f, () =>
            {
                dede.SetBool("speaking", true);
                source.PlayOneShot(dedeClips[1]);
                DelayedAction(7.5f, () =>
                {
                    dede.SetBool("speaking", false);
                });
            });
        DelayedAction(55f, () =>
           {
               source.PlayOneShot(ebreheClips[2]);
               ebrehe.Play("showing-forward");
           });

        // Dede Speaks for a while
        DelayedAction(57.8f, () =>
            {
                dede.SetBool("speaking", true);
                source.PlayOneShot(dedeClips[2]);
                DelayedAction(2f, () =>
                {
                    dede.SetTrigger("show-forward");
                    DelayedAction(2f, () => dede.SetTrigger("show-backward"));
                });
                DelayedAction(6f, () =>
                {
                    dede.SetBool("speaking", false);
                });
            });
    }

    internal void EbreheExit()
    {
        dede.SetInteger("scene number", scene);
        dede.SetTrigger("scene enter");
    }

}
