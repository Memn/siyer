using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class UserManager : MonoBehaviour
{
    public bool IsConnected2GoogleServices;
    private ILocalUser _localUser;

    private static UserManager _instance;

    public static UserManager Instance
    {
        get { return _instance ?? (_instance = new GameObject("UserManager").AddComponent<UserManager>()); }
    }


    private void Awake()
    {
        _localUser = Social.localUser;
        DontDestroyOnLoad(gameObject);
    }

    public void Init()
    {
        var conf = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
        PlayGamesPlatform.InitializeInstance(conf);
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.DebugLogEnabled = true;
    }

    private void Start()
    {
        Connect2GoogleServices();
    }

    // Connection
    public bool Connect2GoogleServices()
    {
        if (!IsConnected2GoogleServices)
            _localUser.Authenticate(success =>
            {
                // On Connection
                IsConnected2GoogleServices = success;
            });
        OpenSave(false); // Load
        return IsConnected2GoogleServices;
    }

    // Achievements
    public void ToAchievements()
    {
        if (!_localUser.authenticated) return;
        Social.ShowAchievementsUI();
    }

    // Leaderboards
    public void ToLeaderboards()
    {
        if (!_localUser.authenticated) return;
        Social.ShowLeaderboardUI();
    }

    // Save/Load Game
    private Game _game;

    public Game Game
    {
        get { return _game ?? (_game = LocalOpenSave()); }
    }

    private bool isSaving = false;

    private void OpenSave(bool saving)
    {
        isSaving = saving;
        // save/load local first
        _game = LocalOpenSave();
        if (!Social.localUser.authenticated) return;

        var savedGame = ((PlayGamesPlatform) Social.Active).SavedGame;
        savedGame.OpenWithAutomaticConflictResolution("Siyer", DataSource.ReadCacheOrNetwork,
                                                      ConflictResolutionStrategy.UseMostRecentlySaved, SaveGameOpen);
    }

    private Game LocalOpenSave()
    {
        if (!isSaving) return Util.LocalLoad(); // reading

        // writing
        Util.LocalSave(_game);
        return _game;
    }

    private void SaveGameOpen(SavedGameRequestStatus status, ISavedGameMetadata metadata)
    {
        if (status != SavedGameRequestStatus.Success) return;
        if (isSaving)
        {
            Util.CloudSave(metadata, _game);
        }
        else
        {
            var game = Util.CloudLoad(metadata);
            if (game.SaveTime < _game.SaveTime)
            {
                // cloud is behind, sync
                Util.CloudSave(metadata, _game);
            }
        }
    }
}