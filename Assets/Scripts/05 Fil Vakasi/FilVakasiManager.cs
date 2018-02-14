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

    // Use this for initialization
    void Start()
    {
        Invoke("StartScene", 2);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("02 Siyer");
        }
    }

    void StartScene()
    {
        ebrehe.SetInteger("scene number", scene);
        ebrehe.SetTrigger("scene enter");
    }

    internal void EbreheExit()
    {
        dede.SetInteger("scene number", scene);
        dede.SetTrigger("scene enter");
    }

    internal void DedeExit()
    {
        scene++;
        if (scene < 4)
            StartScene();
    }
}
