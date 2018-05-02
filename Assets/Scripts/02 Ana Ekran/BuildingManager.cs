using System.Linq;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public Building[] Buildings;
    public SpriteRenderer Background;
    public GameObject Board;
    private BoardHandler _boardHandler;

    private void Start()
    {
        _boardHandler = Board.GetComponent<BoardHandler>();
    }

    private Building FindInBuildings(GameObject b)
    {
        if (!b.GetComponent<Building>()) return null;
        var buildingName = b.name;
        return Buildings.FirstOrDefault(building => building.name == buildingName);
    }

    public void Selection(GameObject b)
    {
        var building = FindInBuildings(b);
        if (!building) return;

        Background.color = Color.gray;
        Board.SetActive(true);
        _boardHandler.ShowBuilding(building);
    }

    public void EnlightAll()
    {
        Background.color = Color.white;
    }
}