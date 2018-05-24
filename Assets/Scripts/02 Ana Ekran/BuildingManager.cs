using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class BuildingManager : MonoBehaviour
{
    public Sprite UnderConstruction;
    public SpriteRenderer Background;
    public GameObject Board;


    private BoardHandler _boardHandler;
    private Building[] _buildings;

    private void Awake()
    {
        _buildings = GetComponentsInChildren<Building>();
    }

    private void Start()
    {
        _boardHandler = Board.GetComponent<BoardHandler>();
    }

    private Building FindInBuildings(GameObject b)
    {
//        if (!b.GetComponent<Building>() || _buildings == null) return null;
//        var buildingName = b.name;
//        return _buildings.FirstOrDefault(building => building.name == buildingName);
        return b.GetComponent<Building>();

    }

    public void Selection(GameObject b)
    {
        Debug.Log("Object name: "+b.name);
        var building = FindInBuildings(b);
        if (!building) return;

        
        Background.color = Color.gray;
        Board.SetActive(true);
        _boardHandler.ShowBuilding(building);
    }

    [UsedImplicitly]
    public void EnlightAll()
    {
        Background.color = Color.white;
    }

    public void LockingAdjustments(IAchievement[] gameAchievements)
    {
        foreach (var building in _buildings)
        {
            var first = gameAchievements.FirstOrDefault(achievement => building.BuildingID == achievement.id);
            if (first == null) continue;

            building.GetComponent<SpriteRenderer>().sprite = first.completed ? building.ActualPhoto : UnderConstruction;
            building.GetComponent<SpriteRenderer>().color = first.completed ? Color.white : Color.gray;
        }
    }
}