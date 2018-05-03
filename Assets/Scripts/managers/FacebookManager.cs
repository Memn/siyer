using System;
using System.Collections.Generic;
using Facebook.MiniJSON;
using Facebook.Unity;
using UnityEngine;

// ReSharper disable MemberCanBeMadeStatic.Local

// ReSharper disable MemberCanBeMadeStatic.Global

public class FacebookManager : MonoBehaviour
{
    private static FacebookManager _instance;

    public static FacebookManager Instance
    {
        get { return _instance ?? (_instance = new GameObject("FacebookManager").AddComponent<FacebookManager>()); }
    }

    public bool IsLoggedIn
    {
        get { return FB.IsLoggedIn; }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // ReSharper disable once InconsistentNaming
    public void InitFB()
    {
        if (!FB.IsInitialized)
            FB.Init(() =>
            {
                if (FB.IsInitialized)
                    FB.ActivateApp();
                else
                    Debug.LogError("Facebook init error!");
            }, isGameShown => Time.timeScale = !isGameShown ? 0 : 1);
        else
            FB.ActivateApp();
    }

    public void LoadProfile(FacebookDelegate<IGraphResult> basicProfileCallback = null,
        FacebookDelegate<IGraphResult> profilePicCallback = null, FacebookDelegate<IGraphResult> friendsCallback = null)
    {
        if (!FB.IsLoggedIn) return;
        if (basicProfileCallback != null)
            FB.API("/me?fields=name", HttpMethod.GET, basicProfileCallback);
        if (profilePicCallback != null)
            FB.API("/me/picture?type=square&width=128&height=128", HttpMethod.GET, profilePicCallback);
        if (friendsCallback != null)
            FB.API("me/friends", HttpMethod.GET, friendsCallback);
    }

    public void LogIn()
    {
        var permissions = new List<string> {"public_profile", "email", "user_friends"};
        FB.LogInWithReadPermissions(permissions, ButtonsController.Instance.LoginCallback);
    }

    public void LogOut()
    {
        FB.LogOut();
    }

    public void Share()
    {
        var contentUrl = new Uri("http://www.gayeakademi.com");
        const string contentTitle = "Hey Look what I am sharing";
        const string contentDescription = "Siyer is a good game to share";
        var photoUrl = new Uri("http://www.gayeakademi.com/images/logo.png");

        FB.ShareLink(contentUrl, contentTitle, contentDescription, photoUrl);
    }

    public void FacebookGameRequest()
    {
        FB.AppRequest("Hey Look what I am Requesting", title: "Siyer Request");
    }

    public void FacebookInvite()
    {
        FB.Mobile.AppInvite(new Uri("https://play.google.com/apps/testing/com.gayeakademi.siyer"));
    }

    public void GetFriendsPlayingThisGame()
    {
        FB.API("me/friends", HttpMethod.GET, result =>
        {
            var dictionary = (Dictionary<string, object>) Json.Deserialize(result.RawResult);
            var friendsList = (List<object>) dictionary["data"];
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
        var leaders = new List<User>();

        foreach (var score in Util.DeserializeScores(result.RawResult))
        {
            var entry = (Dictionary<string, object>) score;
            var user = (Dictionary<string, object>) entry["user"];
            leaders.Add(new User(user["name"].ToString(), entry["score"].ToString()));
        }

//        Leaderboard = leaders;
    }

    public void SetScore()
    {
        // var scoreData = new Dictionary<string, string>();
        // scoreData["score"] = 20.ToString();
        // FB.API("/me/scores", HttpMethod.POST, delegate (IGraphResult result)
        //     {
        //         Debug.Log(result.RawResult);
        //     },
        //     scoreData);
    }
}