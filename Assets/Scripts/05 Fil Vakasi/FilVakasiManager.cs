using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FilVakasiManager : MonoBehaviour {

	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("02 Siyer");
        }
	}
}
