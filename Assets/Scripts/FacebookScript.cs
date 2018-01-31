using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;


public class FacebookScript : MonoBehaviour
{

    public Text FriendsText;
    private void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(() =>
            {
                if (FB.IsInitialized)
                    FB.ActivateApp();
                else
                    Debug.LogError("Facebook init error!");

            },
            isGameShown =>
            {
                if (!isGameShown)
                    Time.timeScale = 0;
                else
                    Time.timeScale = 1;
            });

        }
        else
            FB.ActivateApp();
    }

    #region Login / Logout
    public void FacebookLogin()
    {
        var permissions = new List<string>() { "public_profile", "email", "user_friends" };
        FB.LogInWithReadPermissions(permissions);
    }
    public void FacebookLogout()
    {
        FB.LogOut();
    }
    public void FacebookShare()
    {
        Uri contentURL = new System.Uri("http://www.gayeakademi.com");
        string contentTitle = "Hey Look what I am sharing";
        string contentDescription = "Siyer is a good game to share";
        Uri photoURL = new System.Uri("http://www.gayeakademi.com/images/logo.png");

        FB.ShareLink(contentURL, contentTitle, contentDescription, photoURL);
    }
    #endregion

    #region Inviting
    public void FacebookGameRequest()
    {
        FB.AppRequest("Hey Look what I am Requesting", title: "Siyer Request");
    }
    public void FacebookInvite()
    {
        FB.Mobile.AppInvite(new System.Uri("https://play.google.com/apps/testing/com.gayeakademi.siyer"));
    }

    #endregion

    public void GetFriendsPlayingThisGame()
    {
        string query = "me/friends";
        FB.API(query, HttpMethod.GET, result =>
        {
            var dictionary = (Dictionary<string, object>)Facebook.MiniJSON.Json.Deserialize(result.RawResult);
            var friendsList = (List<object>)dictionary["data"];
            FriendsText.text = string.Empty;
            foreach (var dict in friendsList)
            {
                FriendsText.text += ((Dictionary<string, object>)dict)["name"];
            }

        });

    }
}
