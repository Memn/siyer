using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

[Serializable]
public class User
{
    public string FacebookID;
    public string Name;
    public int Score;
    public Sprite ProfilePic;

    public List<Achievement> Achievements;
    public Dictionary<string, string> Friends;


    public static User Default = new User();

    public User(string name, string score)
    {
        Name = name;
        Score = int.Parse(score);
        Friends = new Dictionary<string, string>();
        Achievements = new List<Achievement>();
    }

    public User(User user)
    {
        Name = user.Name;
        Score = user.Score;
        ProfilePic = user.ProfilePic;
        Friends = new Dictionary<string, string>(user.Friends);
        Achievements = new List<Achievement>();
        Achievements = user.Achievements;
    }

    private User()
    {
        Name = "guest";
        Score = 0;
        ProfilePic = null;
        Friends = new Dictionary<string, string>();
        Achievements = new List<Achievement>();
    }
}