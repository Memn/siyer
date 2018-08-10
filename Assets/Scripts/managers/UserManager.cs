#if UNITY_ANDROID && !UNITY_EDITOR
using GooglePlayGames;
using GooglePlayGames.BasicApi;
#endif
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

        Social.localUser.Authenticate(success => GameSocialHelper.SyncUser(_game, success));
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    internal static void Reward(CommonResources.Building building, int score)
    {
        var reward = Game.RewardOf(building);
        System.Diagnostics.Debug.Assert(reward != null, "reward != null");
        Instance.UnlockAchievement(reward, score);
    }

    public void UnlockAchievement(string id, int score)
    {
        var achievement = _game.AchievementOf(id) as AchievementDto;
        System.Diagnostics.Debug.Assert(achievement != null, "achievement != null");
        if (achievement.completed) return;
        achievement.percentCompleted = 100;
        LogUtil.Log(id + " unlocking...");
#if UNITY_EDITOR
        achievement.completed = true;
        GameSocialHelper.Unlock(_game, achievement);
        ReportScore(score);
#else
        LogUtil.Log(id + " reporting unlock ");
        achievement.ReportProgress(success =>
        {
            if (success)
            {          
                GameSocialHelper.Unlock(_game, achievement);
                ReportScore(score);
                CheckLevelUp(true);
            }
            LogUtil.Log(id + " unlocked successfully or not: " + success);
        });
#endif
    }

    internal static void ReportScore(int score)
    {
        GameSocialHelper.ReportScore(Game, score);
#if !UNITY_EDITOR
        Social.ReportScore(score, SiyerResources.leaderboard_genel, scoreSuccess =>
        {
            if (!scoreSuccess)
            {
                LogUtil.Log("Error, when reporting score!");
            }
        });
#endif
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
}