using System;
using UnityEngine;

[Serializable]
public class Building : MonoBehaviour
{
    public SceneManagementUtil.Scenes Scene;
    public string BuildingID;
    public string BuildingName;
    public string Info;
    public Sprite ActualPhoto;
    
    
    public Sprite Photo
    {
        get { return gameObject.GetComponent<SpriteRenderer>().sprite; }
    }
}