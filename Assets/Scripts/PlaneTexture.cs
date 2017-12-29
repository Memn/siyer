using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneTexture : MonoBehaviour
{

    public Texture myTexture;
    // Use this for initialization
    void Start()
    {
        this.GetComponent<Renderer>().material.SetTexture("_texture", myTexture);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
