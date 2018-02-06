using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public float loadMainMenuAfter;
    

    void Start()
    {
        if (loadMainMenuAfter > 0)
            Invoke("LoadMainMenu", loadMainMenuAfter);
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("01 MainMenu");
    }

    public void QuitRequest()
    {
        Application.Quit();

    }


}
