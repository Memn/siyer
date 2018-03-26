using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SplashScreenManager : MonoBehaviour
{
    public float loadMainMenuAfter;

    void Start()
    {
        if (loadMainMenuAfter > 0)
            Invoke("LoadMainMenu", loadMainMenuAfter);
    }
    public void LoadMainMenu()
    {   
        SceneManagementUtil.Load(SceneManagementUtil.Scenes.Izometrik);
    }
}
