using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ebrehe : MonoBehaviour
{

    public GameObject EyeBorrow;
 
    void Angry(bool isAngry)
    {
        EyeBorrow.SetActive(isAngry);
    }


}
