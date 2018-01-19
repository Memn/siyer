using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BackButtonController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
            gameObject.SetActive(false);
    }
}
