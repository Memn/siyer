using System.Collections.Generic;
using managers;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
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
        ProgressManager.Instance.UnlockAchievement(CommonResources.Levels(1), 100);
        ProgressManager.Instance.CheckLevelUp(true);
        BuildingManager.LockingAdjustments();
    }

    public void ShowLevelQuestsInfo()
    {
        Util.ClearChildren(LevelQuestObjectParent.transform);
        Level.text = CommonResources.LevelsText(ScoreManager.Instance.Level);
        var levelQuests = AchievementsManager.Instance.CurrentLevelAchievementCompletions;

        var list = new List<CommonResources.Duty>();
        foreach (var levelQuest in levelQuests)
        {
            var memberObj = Instantiate(LevelQuestPrefab, Vector3.zero, Quaternion.identity);
            memberObj.transform.SetParent(LevelQuestObjectParent.transform);
            memberObj.transform.localScale = Vector3.one;
            memberObj.GetComponentInChildren<Text>().text = levelQuest.Value.Title;
            var image = memberObj.GetComponentInChildren<Image>();
            image.sprite = levelQuest.Key ? _completed : _notCompleted;
            image.color = levelQuest.Key ? Color.green : Color.red;
        }

        ShowLevelQuestsInfoButton.SetActive(false);
        LevelQuestsInfoScreen.SetActive(true);
    }
}