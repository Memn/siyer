using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class BoardHandler : MonoBehaviour
{
    private Image _photo;
    private Text _title;
    private Text _info;

    private CommonResources.Building _targetScene;
    private Button _go;
    public GameObject Sign;

    private void Awake()
    {
        foreach (Transform child in gameObject.transform)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (child.name)
            {
                case "Title":
                    _title = child.GetComponent<Text>();
                    break;
                case "Photo":
                    _photo = child.GetComponent<Image>();
                    break;
                case "Info":
                    _info = child.GetComponent<Text>();
                    break;
                case "Enter":
                    _go = child.GetComponent<Button>();
                    break;
            }
        }
    }

    public void ShowBuilding(Building building)
    {
        if (!building) return;

        _photo.sprite = building.Photo;
        _title.text = building.BuildingName;
        _info.text = building.Info;
        _targetScene = building.Resource;
        _go.interactable = building.Achieved;
        Sign.SetActive(building.Achieved);
    }

    [UsedImplicitly]
    public void GoScene()
    {
        SceneManagementUtil.Load(_targetScene);
    }
}