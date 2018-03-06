using System.Linq;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public Building[] Buildings;

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
        }
    }

    private void UnselectAll()
    {
        foreach (var building1 in Buildings)
        {
            building1.Unselect();
        }
    }

    private Building FindInBuildings(GameObject b)
    {
        if (!b.GetComponent<Building>()) return null;
        var buildingName = b.name;
        return Buildings.FirstOrDefault(building => building.name == buildingName);
    }
}