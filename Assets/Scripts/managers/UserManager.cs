#if UNITY_ANDROID && !UNITY_EDITOR
using GooglePlayGames;
using GooglePlayGames.BasicApi;
#endif
using System.Collections.Generic;
using System.IO;
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

    public void Init()
    {
        _game = Game.Load();
        // Load Quests if not have
        if (!File.Exists(Util.QuestsFile))
            StartCoroutine(Util.DownloadFile(Util.QuestsReference, Util.QuestsFile));

#if UNITY_ANDROID && !UNITY_EDITOR
        Debug.Log("Play Games Activation started");
        var config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
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

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public static IEnumerable<KeyValuePair<bool, string>> GetCurrentLevelAchievementCompletions()
    {
        return Game.LevelDuties.Select(duty => new KeyValuePair<bool, string>(Game.IsAchieved(duty.Reward), duty.Title))
            .ToList();
    }

    internal static void Reward(CommonResources.Building building, int score)
    {
        var duties = CommonResources.DutyOf(Game.Level);
        var reward = duties.Find(duty => duty.Building == building).Reward;
        if (reward == null)
        {
            Debug.Log("Reward not specified.");
            return;
        }

        Instance.UnlockAchievement(reward, score);
    }

    public void UnlockAchievement(string id, int score)
    {
#if UNITY_EDITOR
        var achievement = _game.AchievementOf(id) as AchievementDto;
        System.Diagnostics.Debug.Assert(achievement != null, "achievement != null");
        achievement.percentCompleted = 100;
        achievement.completed = true;
        _game.UnlockedAchievement(achievement);
        ReportScore(score);
#else
        var achievement = _game.AchievementOf(id);
        achievement.percentCompleted = 100;
        achievement.ReportProgress(success =>
        {
            if (success)
            {
                _game.UnlockedAchievement(achievement);
                ReportScore(score);
                CheckLevelUp();
            }
            Debug.Log(id + " unlocked successfully or not: " + success);
        });
#endif
    }

    internal static void ReportScore(int score)
    {
        Game.ReportScore(score);
#if !UNITY_EDITOR
        Social.ReportScore(score, SiyerResources.leaderboard_genel, scoreSuccess =>
        {
            if (!scoreSuccess)
            {
                Debug.Log("Error, when reporting score!");
            }
        });
#endif
    }

    public static void SyncUserLater(float time)
    {
        Instance.Invoke("Sync", time);
    }

    public static void CheckLevelUp()
    {
        var levelQuests = GetCurrentLevelAchievementCompletions();
        if (levelQuests.All(pair => pair.Key))
            LevelUp();
    }

    private static void LevelUp()
    {
        if (Game.Level == 5) return;
        Game.Level++;
        Instance.UnlockAchievement(CommonResources.Levels(Game.Level), 250);
        Instance.UnlockAchievement(CommonResources.Stories(Game.Level), 50);
        Instance.Invoke("CheckLocks", 0.8f);
    }

    // ReSharper disable once MemberCanBeMadeStatic.Local
    public void CheckLocks()
    {
        if (FindObjectOfType<BuildingManager>())
            FindObjectOfType<BuildingManager>().LockingAdjustments(Game.Achievements);
    }

    private void Sync()
    {
        _game.Sync(Social.localUser);
    }
}