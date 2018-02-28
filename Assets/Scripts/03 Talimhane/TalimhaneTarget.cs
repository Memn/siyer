using UnityEngine;

public class TalimhaneTarget : MonoBehaviour
{
    float xMin = -12.0f;
    float xMax = 12.0f;
    float zMin = -350.0f; // -400-350 = 50 meters far
    float zMax = -250.0f; //  -400-250  = 150 meters far

    public Terrain terrain;
    private TalimhaneManager _manager;

    void Start()
    {
        _manager = FindObjectOfType<TalimhaneManager>();
        PutGround();
        _manager.RecalculateDistance();
    }

    private void PutGround()
    {
        transform.position =
            new Vector3(transform.position.x, HeightAtTerrain(transform.position), transform.position.z);
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
        _manager.RecalculateDistance();
    }
}