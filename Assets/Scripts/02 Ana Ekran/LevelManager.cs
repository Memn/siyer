using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Debug = System.Diagnostics.Debug;

public class LevelManager : MonoBehaviour
{
    private Game _game;

    // 1. Kabe 
    // 2. ..
    public BuildingManager BuildingManager;
    public GameObject LevelQuestsInfoScreen;
    public GameObject LevelQuestPrefab;
    public GameObject LevelQuestObjectParent;
    public GameObject ShowLevelQuestsInfoButton;
    public Text Level;
    [SerializeField] private Sprite _completed;
    [SerializeField] private Sprite _notCompleted;

    private void Start()
    {
        _game = UserManager.Game;
        UserManager.Instance.UnlockAchievement(CommonResources.Levels(1), 100);
        BuildingManager.LockingAdjustments();
        UserManager.CheckLevelUp(true);
    }

    public void ShowLevelQuestsInfo()
    {
        Util.ClearChildren(LevelQuestObjectParent.transform);
        Level.text = CommonResources.LevelsText(UserManager.Game.Level);
        var levelQuests = UserManager.Game.CurrentLevelAchievementCompletions;

        foreach (var levelQuest in levelQuests)
        {
            var memberObj = Instantiate(LevelQuestPrefab, Vector3.zero, Quaternion.identity);
            memberObj.transform.SetParent(LevelQuestObjectParent.transform);
            memberObj.transform.localScale = Vector3.one;
            memberObj.GetComponentInChildren<Text>().text = levelQuest.Value;
            var image = memberObj.GetComponentInChildren<Image>();
            image.sprite = levelQuest.Key ? _completed : _notCompleted;
            image.color = levelQuest.Key ? Color.green : Color.red;
        }

        ShowLevelQuestsInfoButton.SetActive(false);
        LevelQuestsInfoScreen.SetActive(true);
    }
}