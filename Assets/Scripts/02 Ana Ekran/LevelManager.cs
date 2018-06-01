using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private Sprite _completed;
    [SerializeField] private Sprite _notCompleted;

    private void Start()
    {
        _game = UserManager.Game;
//        BuildingManager.LockingAdjustments(_game.Achievements);
        
        var levelQuests = UserManager.GetCurrentLevelAchievementCompletions();
        if (levelQuests.Any(pair => !pair.Key))    return;
        UserManager.LevelUp();
    }

    public void ShowLevelQuestsInfo()
    {
        Util.ClearChildren(LevelQuestObjectParent.transform);
        var levelQuests = UserManager.GetCurrentLevelAchievementCompletions();
        
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