#if UNITY_ANDROID && !UNITY_EDITOR
using GooglePlayGames;
#endif
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class UserManager : MonoBehaviour
{
    private Game _game;
    private static UserManager _instance;
    public int CurrentLevel = 1;

    public static UserManager Instance
    {
        get { return _instance ?? (_instance = new GameObject("UserManager").AddComponent<UserManager>()); }
    }

    public Game Game
    {
        get { return _game; }
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

    public IAchievementDescription DescriptionOf(string id)
    {
        return Game.DescriptionOf(id);
    }

    public IEnumerable<KeyValuePair<bool, string>> GetCurrentLevelCompletionAchievements()
    {
        var criteriaCompletions = new List<KeyValuePair<bool, string>>();
        List<string> achievementIds;
        if (!Game.LevelCompletionCriterias.TryGetValue(CurrentLevel, out achievementIds)) return criteriaCompletions;
        foreach (var achievementId in achievementIds)
        {
            var description = _game.DescriptionOf(achievementId).unachievedDescription;
            var achievement = _game.AchievementOf(achievementId);
            var completed = achievement != null && achievement.completed;
            criteriaCompletions.Add(new KeyValuePair<bool, string>(completed, description));
        }

        return criteriaCompletions;
    }
}