using System;
using UnityEngine;

[Serializable]
public class Building : MonoBehaviour
{
    public CommonResources.Resource Resource;
    public Sprite ActualPhoto;

    private CommonResources.Description _desc;

    private CommonResources.Description Description
    {
        get
        {
            if (_desc == null)
            {
                CommonResources.Descriptions.TryGetValue(Resource, out _desc);
            }

            return _desc ?? CommonResources.Description.None;
        }
    }

    public string BuildingID
    {
        get { return CommonResources.IdOf(Resource); }
    }


    public string Info
    {
        get { return Description.Info; }
    }


    public string BuildingName
    {
        get { return Description.Title; }
    }

    public Sprite Photo
    {
        get { return gameObject.GetComponent<SpriteRenderer>().sprite; }
    }

    private bool _achieved;

    public bool Achieved
    {
        get { return _achieved; }
        set { _achieved = value; }
    }
}