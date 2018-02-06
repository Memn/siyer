using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SiyerManager : MonoBehaviour
{

    public GameObject fadeInPanel;

    void Awake()
    {
        fadeInPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("01a MainMenu");
        }

    }
}
