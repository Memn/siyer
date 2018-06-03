#if UNITY_ANDROID && !UNITY_EDITOR
using GooglePlayGames;
#endif
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        });
        Debug.Log("Game Init is completed..");
    }

    public static IEnumerable<KeyValuePair<bool, string>> GetCurrentLevelAchievementCompletions()
    {
        return Game.LevelDuties.Select(duty => new KeyValuePair<bool, string>(Game.IsAchieved(duty.Reward), duty.Title))
                   .ToList();
    }
 
    private static void Reward(CommonResources.Building building)
    {
        var duties = CommonResources.DutyOf(Game.Level);
        var reward = duties.Find(duty => duty.Building == building).Reward;
        if (reward == null)
        {
            Debug.Log("Reward not specified.");
            return;
        }

        Instance.UnlockAchievement(reward);
    }

    public void UnlockAchievement(string id)
    {
        var achievement = _game.AchievementOf(id);
        if (achievement == null)
        {
            achievement = Social.CreateAchievement();
            achievement.id = id;
#if UNITY_EDITOR
            achievement.percentCompleted = 100;
            _game.UnlockedAchievement(achievement);
            return;
#endif
        }

        achievement.percentCompleted = 100;
        achievement.ReportProgress((success) =>
        {
            if (success)
            {
                _game.UnlockedAchievement(achievement);
            }

            Debug.Log(id + " unlocked successfully or not: " + success);
        });
    }

    public static void SyncUserLater(float time)
    {
        Instance.Invoke("Sync", time);
    }

    public static void LevelUp()
    {
        Game.Level++;
        Instance.UnlockAchievement(CommonResources.Levels(Game.Level));
        Instance.UnlockAchievement(CommonResources.Stories(Game.Level));
        FindObjectOfType<BuildingManager>().LockingAdjustments(Game.Achievements);
    }

    private void Sync()
    {
        _game.Sync(Social.localUser);
    }

    public static void StorySuccess(CommonResources.Building building)
    {
        Instance.UnlockAchievement(CommonResources.IdOf(building));
    }

    public static void MazeSuccess(int collected, float timerRemaining)
    {
        Debug.Log("Success with " + collected + " collected and remaining time in sec:" + timerRemaining);
        Reward(CommonResources.Building.Abdulmuttalib);
    }

    public static void KelimelikSuccess(int score, float spentTime)
    {
        Debug.Log("Kelimelik success " + score + " remaining time in sec:" + spentTime);
        Reward(CommonResources.Building.DarulErkam);
    }

    public static bool TalimhaneSuccess(int score)
    {
        if (score < 6) return false;
        Reward(CommonResources.Building.EbuTalib);
        if (score == 10)
        {
            Instance.UnlockAchievement(CommonResources.Extras(CommonResources.Building.EbuTalib));
        }
        return true;

    }
}