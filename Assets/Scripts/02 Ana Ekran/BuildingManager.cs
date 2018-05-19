using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

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
        if (!b.GetComponent<Building>() || _buildings == null) return null;
        var buildingName = b.name;
        return _buildings.FirstOrDefault(building => building.name == buildingName);
    }

    public void Selection(GameObject b)
    {
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

    public void LockingAdjustments(Dictionary<string, bool> gameAchievements)
    {
        foreach (var building in _buildings)
        {
            bool unlocked;
            if (!gameAchievements.TryGetValue(building.BuildingID, out unlocked)) continue;
            building.GetComponent<SpriteRenderer>().sprite = unlocked ? building.ActualPhoto : UnderConstruction;
            building.GetComponent<SpriteRenderer>().color = unlocked ? Color.white : Color.gray;
        }
    }
}