using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    public Building[] Buildings;
    public Text SelectedBuildingNameText;

    private void Start()
    {
        SelectedBuildingNameText.text = "";
    }

    public void Select(GameObject b)
    {
        var building = FindInBuildings(b);
        if (!building)
        {
            UnselectAll();
            return;
        }

        if (building.Selected)
        {
            building.GoScene();
        }
        else
        {
            UnselectAll();
            building.Select();
            SelectedBuildingNameText.text = building.BuildingName;
        }
    }

    private void UnselectAll()
    {
        foreach (var building1 in Buildings)
        {
            building1.Unselect();
        }

        SelectedBuildingNameText.text = "";
    }

    private Building FindInBuildings(GameObject b)
    {
        if (!b.GetComponent<Building>()) return null;
        var buildingName = b.name;
        return Buildings.FirstOrDefault(building => building.name == buildingName);
    }
}