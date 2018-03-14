
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    float xMin = -12.0f;
    float xMax = 12.0f;
    float zMin = -350.0f; // -400-350 = 50 meters far
    float zMax = -3100.0f; //  -400-310  = 90 meters far

    public Terrain terrain;

    void Start()
    {
        PutGround();
    }

    private void PutGround()
    {
        transform.position = new Vector3(transform.position.x, HeightAtTerrain(transform.position), transform.position.z);
    }

    float HeightAtTerrain(Vector3 pos)
    {
        return terrain.SampleHeight(pos) + 1.05f;
    }

    public void SetPostion()
    {
        float x = Random.Range(xMin, xMax);
        float z = Random.Range(zMin, zMax);
        float y = HeightAtTerrain(new Vector3(x, transform.position.y, z));
        transform.position = new Vector3(x, y, z);
    }

}
