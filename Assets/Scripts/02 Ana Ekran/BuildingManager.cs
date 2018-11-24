using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public Sprite UnderConstruction;
    public SpriteRenderer Background;
    public GameObject Board;
    public GameObject Sign;

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

    public void Selection(GameObject b)
    {
        var building = b.GetComponent<Building>();
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

    public void LockingAdjustments()
    {
        foreach (var building in _buildings)
        {
            // init case
            if (building.Resource == CommonResources.Building.Kabe)
            {
                UserManager.Instance.UnlockAchievement(building.BuildingID, 100);
                building.Achieved = true;
                continue;
            }

            var first = UserManager.Game.AchievementOf(building.BuildingID);
            var completed = first != null && first.completed;
            building.Achieved = completed;
            building.GetComponent<SpriteRenderer>().sprite = completed ? building.ActualPhoto : UnderConstruction;
            building.GetComponent<SpriteRenderer>().color = completed ? Color.white : Color.gray;
        }
        AddSign(UserManager.Game.CurrentLevelAchievementCompletions);
    }

    private void AddSign(IEnumerable<KeyValuePair<bool, CommonResources.Duty>> currentDuties)
    {
        const string isaretciName = "Isaretci";
        foreach (var build in _buildings)
           foreach (Transform child in build.transform)
               if(child.gameObject.name == isaretciName)
                   Destroy(child.gameObject);
        var leftDutyPair = currentDuties.FirstOrDefault(pair => !pair.Key);
        if (leftDutyPair.Equals(default(KeyValuePair<bool, CommonResources.Duty>))) 
            return; // if no duty left
        var leftDuty = leftDutyPair.Value;
        var building = _buildings.First(b => b.Resource == leftDuty.Building);
        var memberObj = Instantiate(Sign, Vector3.zero, Quaternion.identity);
        memberObj.name = isaretciName;
        memberObj.transform.SetParent(building.transform);
        if (building.Resource == CommonResources.Building.Kabe)
        {
            memberObj.transform.localScale = Vector3.one * 5;
            memberObj.transform.localPosition = new Vector3(1, 4, 0);                        
        }
        else
        {
            memberObj.transform.localScale = Vector3.one * 15;
            memberObj.transform.localPosition = new Vector3(5, 10, 0);            
        }
    }
}