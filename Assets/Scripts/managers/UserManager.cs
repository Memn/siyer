#if UNITY_ANDROID && !UNITY_EDITOR
using GooglePlayGames;
#endif
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UserManager : MonoBehaviour
{
    private Game _game;
    private static UserManager _instance;

    public static UserManager Instance
    {
        get { return _instance ?? (_instance = new GameObject("UserManager").AddComponent<UserManager>()); }
    }

    public static Game Game
    {
        get { return Instance._game; }
    }

    public static int CurrentLevel
    {
        get { return Game.Level; }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Init()
    {
        _game = Game.Load();

#if UNITY_ANDROID && !UNITY_EDITOR
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.DebugLogEnabled = true;
#endif
        Social.localUser.Authenticate(success =>
        {
            Debug.Log(!success
                          ? "Failed to authenticate, continuing with local save."
                          : "Authenticated, checking achievements to sync with local");

            if (!success) return;
            _game.Sync(Social.localUser);
            Social.LoadAchievements(achievements =>
            {
                if (achievements.Length == 0)
                    Debug.Log("Error: no achievements found");
                else
                    _game.Sync(achievements);
            });
            Social.LoadAchievementDescriptions(descriptions =>
            {
                if (descriptions.Length == 0)
                    Debug.Log("Error: no descriptions found");
                else
                    _game.Sync(descriptions);
            });
        });
        Debug.Log("Game Init is completed..");
    }

    public static IEnumerable<KeyValuePair<bool, string>> GetCurrentLevelAchievementCompletions()
    {
        return (from duty in Game.LevelDuties 
                let description = duty.Title 
                let achievement = Game.AchievementOf(duty.Reward) 
                let completed = achievement != null && achievement.completed 
                select new KeyValuePair<bool, string>(completed, description)).ToList();
    }

    public static void UnlockBuilding(CommonResources.Resource building)
    {
        Instance.UnlockAchievement(CommonResources.IdOf(building));
    }

    public void UnlockAchievement(string id)
    {
        var achievement = _game.AchievementOf(id);
        if (achievement == null)
        {
            achievement = Social.CreateAchievement();
            achievement.id = id;
        }

        achievement.percentCompleted = 100;
        achievement.ReportProgress((success) =>
        {
            if (success)
            {
                Social.LoadAchievements(achievements => _game.Sync(achievements));
            }

            Debug.Log(id + " unlocked successfully or not: " + success);
        });
    }

    public static void LevelUp()
    {
        Game.Level++;
//        Instance.UnlockAchievement();
    }

    public static void SyncUserLater(float time)
    {
        Instance.Invoke("Sync", time);
    }

    private void Sync()
    {
        _game.Sync(Social.localUser);
    }


}