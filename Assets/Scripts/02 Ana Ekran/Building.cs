using System;
using UnityEngine;
using UnityEngine.SocialPlatforms;

[Serializable]
public class Building : MonoBehaviour
{
    public SceneManagementUtil.Scenes Scene;
    public string BuildingID;
    public Sprite ActualPhoto;

    private IAchievementDescription _description;

    private IAchievementDescription Description
    {
        get { return _description ?? (_description = UserManager.Instance.Game.DescriptionOf(BuildingID)); }
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
}