#if UNITY_ANDROID && !UNITY_EDITOR
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
#endif
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Debug = System.Diagnostics.Debug;

public class UserManager : MonoBehaviour
{
    private Game _game;
    private dreamloLeaderBoard _dreamloLeaderBoard;
    private static UserManager _instance;

    public static UserManager Instance
    {
        get { return _instance ? _instance : (_instance = new GameObject("UserManager").AddComponent<UserManager>()); }
    }

    public static Game Game
    {
        get { return Instance._game; }
    }

    public void Init()
    {
        _dreamloLeaderBoard = gameObject.AddComponent<dreamloLeaderBoard>();
        _game = GameSaveLoadHelper.Load();
        // Load Quests if not have
        if (!File.Exists(Util.QuestsFile))
        {
            LogUtil.Log("Quests File is missing trying to download...");
            StartCoroutine(Util.DownloadFile(Util.QuestsReference, Util.QuestsFile));
        }

#if UNITY_ANDROID && !UNITY_EDITOR
        LogUtil.Log("Play Games Activation started");
        var config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.DebugLogEnabled = LogUtil.Debugging;
#endif

        Social.localUser.Authenticate(success =>
        {
            LogUtil.Log(!success
                ? "Failed to authenticate, continuing with local save."
                : "Authenticated, checking achievements to sync with local");

            if (success)
                GameSocialHelper.SyncUser(_game);
        });
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    internal static void Reward(CommonResources.Building building, int score)
    {
        var reward = Game.RewardOf(building);
        Debug.Assert(reward != null, "reward != null");
        Instance.UnlockAchievement(reward, score);
    }

    public void UnlockAchievement(string id, int score)
    {
        var achievement = _game.AchievementOf(id) as AchievementDto;
        Debug.Assert(achievement != null, "achievement is null");
        if (achievement.completed) return;
        achievement.percentCompleted = 100;
        LogUtil.Log(id + " unlocking...");
#if UNITY_EDITOR
        ReportProgress(achievement, score);
#else
        achievement.ReportProgress(success =>
        {
            if (success)
                ReportProgress(achievement, score);
            LogUtil.Log(id + " unlocked successfully or not: " + success);
        });
#endif
    }

    private static void ReportProgress(AchievementDto achievement, int score)
    {
        GameSocialHelper.LocalUnlock(Game, achievement);
        ReportScore(score);
        CheckLevelUp(true);
    }

    internal static void ReportScore(int score)
    {
        GameSocialHelper.ReportScore(Game, score);
        UpdateScore();
    }

    public static void CheckLevelUp(bool save)
    {
        if (!Game.CurrentLevelAchievementCompletions.All(achievementCompletions => achievementCompletions.Key)) return;
        LevelUp();
        if (save)
            GameSaveLoadHelper.Save(Game);
    }

    private static void LevelUp()
    {
        if (Game.Level == 5) return;
        Game.Level++;
        Instance.UnlockAchievement(CommonResources.Levels(Game.Level), 250);
        Instance.UnlockAchievement(CommonResources.Stories(Game.Level), 50);
        Instance.Invoke("CheckLocks", 0.8f);
    }

    public void CheckLocks()
    {
        if (FindObjectOfType<BuildingManager>())
            FindObjectOfType<BuildingManager>().LockingAdjustments();
    }

    public static void LoadScores(UnityAction<IEnumerable<dreamloLeaderBoard.Score>> callback)
    {
        Instance._dreamloLeaderBoard.LoadScoresTo(callback);
    }

    public static void LoadMyScore(UnityAction<dreamloLeaderBoard.Score> callback)
    {
        LogUtil.Log("Loading score from server for user: " + Game.UserName);
        Instance._dreamloLeaderBoard.LoadSingleScore(Game.UserName, callback);
    }

    public static void UpdateScore()
    {
        LogUtil.Log("Send score to server user: " + Game.UserName + ", score:" + Game.Score);
        Instance._dreamloLeaderBoard.AddScore(Game.UserName, Game.Score);
    }
}