using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Siyer : MonoBehaviour
{



    public GameObject sceneManager;
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            sceneManager.GetComponent<SceneLoader>().LoadScene(SiyerScenes.Arching);
        }
    }


}
