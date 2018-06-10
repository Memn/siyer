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

    private static void Reward(CommonResources.Building building, int score)
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
        var achievement = _game.AchievementOf(id);
        if (achievement == null)
        {
            // no store connection yet!
            achievement = Social.CreateAchievement();
            achievement.id = id;
            achievement.percentCompleted = 100;
            _game.UnlockedAchievement(achievement, score);
            return;
        }

        achievement.percentCompleted = 100;
        achievement.ReportProgress(success =>
        {
            if (success)
            {
                _game.UnlockedAchievement(achievement, score);
                Social.ReportScore(score, SiyerResources.leaderboard_genel, scoreSuccess =>
                {
                    if (scoreSuccess)
                    {
                        Debug.Log("Score is updated by:" + score);
                    }
                });
                CheckLevelUp();
            }

            Debug.Log(id + " unlocked successfully or not: " + success);
        });
    }

    public static void SyncUserLater(float time)
    {
        Instance.Invoke("Sync", time);
    }

    public static void CheckLevelUp()
    {
        var levelQuests = GetCurrentLevelAchievementCompletions();
        if (levelQuests.All(pair => pair.Key))
        {
            LevelUp();
        }
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

    public static void StorySuccess(CommonResources.Building building)
    {
        Instance.UnlockAchievement(CommonResources.IdOf(building), 100);
    }

    public static void MazeSuccess(int collected, float timerRemaining)
    {
        Debug.Log("Success with " + collected + " collected and remaining time in sec:" + timerRemaining);
        Reward(CommonResources.Building.Abdulmuttalib, (int) (timerRemaining * 5));
    }

    public static void KelimelikSuccess(int score, float spentTime)
    {
        Debug.Log("Kelimelik success " + score + " remaining time in sec:" + spentTime);
        Reward(CommonResources.Building.DarulErkam, (int) (score - spentTime / 10));
    }

    public static bool TalimhaneSuccess(int score)
    {
        if (score < 6) return false;
        Reward(CommonResources.Building.EbuTalib, score * 50);
        if (score == 10)
        {
            Instance.UnlockAchievement(CommonResources.Extras(CommonResources.Building.EbuTalib), 250);
        }

        return true;
    }

    public static bool BilgiYarismasiEnd(int score, float timer)
    {
        if (score < 6) return false;
        Reward(CommonResources.Building.Omer, (int) (score * 50 - (timer / 10)));
        if (score > 8)
        {
            Instance.UnlockAchievement(CommonResources.Extras(CommonResources.Building.Omer), 250);
        }

        return true;
    }
}