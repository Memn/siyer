#if UNITY_ANDROID && !UNITY_EDITOR
using GooglePlayGames;
#endif
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class UserManager : MonoBehaviour
{
    private Game _game;
    private static UserManager _instance;

    public static UserManager Instance
    {
        get { return _instance ?? (_instance = new GameObject("UserManager").AddComponent<UserManager>()); }
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
        Social.localUser.Authenticate(ProcessAuthentication);
        Debug.Log("Game Init is completed..");
    }

    private void ProcessAuthentication(bool success)
    {
        if (success)
        {
            Debug.Log("Authenticated, checking achievements to sync with local");
            // Request loaded achievements, and register a callback for processing them
            _game.Sync(Social.localUser);
            Social.LoadAchievements(ProcessLoadedAchievements);
            Social.LoadAchievementDescriptions(ProcessLoadedAchievementDescriptions);
        }
        else
        {
            Debug.Log("Failed to authenticate, continuing with local save.");
        }
    }

    private void ProcessLoadedAchievementDescriptions(IAchievementDescription[] descriptions)
    {
        if (descriptions.Length == 0)
            Debug.Log("Error: no achievements found");
        else
            _game.Sync(descriptions);
    }

    private void ProcessLoadedAchievements(IAchievement[] achievements)
    {
        if (achievements.Length == 0)
            Debug.Log("Error: no achievements found");
        else
            _game.Sync(achievements);
    }
}