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

    public void LockingAdjustments(IAchievement[] gameAchievements)
    {
        foreach (var building in _buildings)
        {
            // init case
            if (building.Scene == SceneManagementUtil.Scenes.Kabe)
            {
                building.Achieved = true;
                continue;
            }

            var first = gameAchievements.FirstOrDefault(achievement => building.BuildingID == achievement.id);
            var completed = first != null && first.completed;
            building.Achieved = completed;
            building.GetComponent<SpriteRenderer>().sprite = completed ? building.ActualPhoto : UnderConstruction;
            building.GetComponent<SpriteRenderer>().color = completed ? Color.white : Color.gray;
        }
    }
}