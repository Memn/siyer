using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    public Building[] Buildings;
    public SpriteRenderer Background;
    public Text SelectedBuildingNameText;

    private void Start()
    {
        SelectedBuildingNameText.text = "";
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
        if (!building)
        {
            EnlightAll();
            return;
        }

        if (building.Selected)
        {
            building.GoScene();
        }
        else
        {
            DarkenAll();
            building.Selection();
            SelectedBuildingNameText.text = building.BuildingName;
        }
    }

    private void EnlightAll()
    {
        foreach (var building1 in Buildings)
        {
            building1.Enlight();
        }

        Background.color = Color.white;
        SelectedBuildingNameText.text = "";
    }

    private void DarkenAll()
    {
        foreach (var building1 in Buildings)
        {
            building1.Darken();
        }

        Background.color = Color.gray;
        SelectedBuildingNameText.text = "";
    }
}