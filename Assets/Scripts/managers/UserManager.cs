using GooglePlayGames;
using UnityEngine;

public class UserManager : MonoBehaviour
{
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
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.DebugLogEnabled = true;
        Social.localUser.Authenticate((bool success) =>
                                          Debug.Log("Connecttion " + (success ? "connected" : "not connected")));
    }

    private void Start()
    {
        Connect2GoogleServices();
    }

    public bool IsConnected2GoogleServices;

    public bool Connect2GoogleServices()
    {
        if (!IsConnected2GoogleServices)
        {
            Social.localUser.Authenticate((success) => { IsConnected2GoogleServices = success; });
        }

        return IsConnected2GoogleServices;
    }

    public void ToAchievements()
    {
        if (Social.localUser.authenticated)
        {
            Social.ShowAchievementsUI();
        }
    }
    
    
}