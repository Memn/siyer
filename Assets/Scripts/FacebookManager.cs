using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;

public class FacebookManager : MonoBehaviour
{

    private static FacebookManager _instance;

    public static FacebookManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject fbm = new GameObject("FacebookManager");
                fbm.AddComponent<FacebookManager>();
            }
            return _instance;
        }
    }

    public bool IsLoggedIn { get; set; }
    public string ProfileName { get; set; }
    public Sprite ProfilePic { get; set; }

    public List<User> Leaderboard { get; set; }
    public string AppLink { get; }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        _instance = this;
    }
    public void InitFB()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(SetInit, OnHideUnity);
        }
        else
        {
            FB.ActivateApp();
            IsLoggedIn = FB.IsLoggedIn;
        }
    }

    void SetInit()
    {
        if (FB.IsInitialized)
            FB.ActivateApp();
        else
            Debug.LogError("Facebook init error!");

    }

    void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void GetProfile()
    {
        if (FB.IsLoggedIn)
        {
            FB.API("/me?fields=name", HttpMethod.GET, UpdateProfileName);
            FB.API("/me/picture?type=square&width=128&height=128", HttpMethod.GET, UpdateProfilePic);
        }
    }
    void UpdateProfileName(IResult result)
    {
        if (string.IsNullOrEmpty(result.Error))
        {
            ProfileName = result.ResultDictionary["name"].ToString();
        }
        else
        {
            Debug.LogError("Cannot load user name");
            Debug.LogError(result.Error);
        }

    }
    void UpdateProfilePic(IGraphResult result)
    {
        if (string.IsNullOrEmpty(result.Error))
        {
            ProfilePic = Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2());
        }
        else
        {
            Debug.LogError("Profile Picture cannot be loaded.");
            Debug.LogError(result.Error);
        }
    }

    public void LogIn()
    {
        var permissions = new List<string>() { "public_profile", "email", "user_friends", "publish_actions" };
        FB.LogInWithReadPermissions(permissions);
        FacebookManager.Instance.IsLoggedIn = true;
    }
    public void LogOut()
    {
        FB.LogOut();
        FacebookManager.Instance.IsLoggedIn = false;
    }

    public void Share()
    {
        Uri contentURL = new System.Uri("http://www.gayeakademi.com");
        string contentTitle = "Hey Look what I am sharing";
        string contentDescription = "Siyer is a good game to share";
        Uri photoURL = new System.Uri("http://www.gayeakademi.com/images/logo.png");

        FB.ShareLink(contentURL, contentTitle, contentDescription, photoURL);
    }
    public void FacebookGameRequest()
    {
        FB.AppRequest("Hey Look what I am Requesting", title: "Siyer Request");
    }
    public void FacebookInvite()
    {
        FB.Mobile.AppInvite(new System.Uri("https://play.google.com/apps/testing/com.gayeakademi.siyer"));
    }


    public void GetFriendsPlayingThisGame()
    {
        string query = "me/friends";
        FB.API(query, HttpMethod.GET, result =>
        {
            var dictionary = (Dictionary<string, object>)Facebook.MiniJSON.Json.Deserialize(result.RawResult);
            var friendsList = (List<object>)dictionary["data"];
            // FriendsText.text = string.Empty;
            // foreach (var dict in friendsList)
            // {
            //     FriendsText.text += ((Dictionary<string, object>)dict)["name"];
            // }

        });

    }

    public void QueryLeaderboard()
    {
        FB.API("/app/scores?fields=score,user.limit(10)", HttpMethod.GET, ScoresCallback);
    }
    private void ScoresCallback(IGraphResult result)
    {
        List<User> leaders = new List<User>();

        foreach (var score in Util.DeserializeScores(result.RawResult))
        {
            var entry = (Dictionary<string, object>)score;
            var user = (Dictionary<string, object>)entry["user"];
            leaders.Add(new User(user["name"].ToString(), entry["score"].ToString()));
        }
        Leaderboard = leaders;
    }

    public void SetScore()
    {
        var scoreData = new Dictionary<string, string>();
        scoreData["score"] = 20.ToString();
        FB.API("/me/scores", HttpMethod.POST, delegate (IGraphResult result)
            {
                Debug.Log(result.RawResult);
            },
            scoreData);

    }

}
