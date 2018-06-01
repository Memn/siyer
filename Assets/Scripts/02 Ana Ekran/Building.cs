using System;
using UnityEngine;
using UnityEngine.SocialPlatforms;

[Serializable]
public class Building : MonoBehaviour
{
    public CommonResources.Resource Resource;
    public Sprite ActualPhoto;

    public string BuildingID
    {
        get { return CommonResources.IdOf(Resource); }
    }

    private IAchievementDescription _description;
    private bool _achieved;

    private IAchievementDescription Description
    {
        get { return _description ?? (_description = UserManager.Game.DescriptionOf(BuildingID)); }
    }

    public string Info
    {
        get { return Description == null ? "" : Description.achievedDescription; }
    }

    public string BuildingName
    {
        get { return Description == null ? "" : Description.title; }
    }

    public Sprite Photo
    {
        get { return gameObject.GetComponent<SpriteRenderer>().sprite; }
    }

    public bool Achieved
    {
        get { return _achieved; }
        set { _achieved = value; }
    }
}