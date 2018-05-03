using System.Collections.Generic;
using UnityEngine;

public class User
{
    public string FacebookID;
    public string Name;
    public int Score;
    public Sprite ProfilePic;
    public List<GameAchievement> Achievements;
    public Dictionary<string, string> Friends;


    public static readonly User Default = new User();

    public User(string name, string score)
    {
        Name = name;
        Score = int.Parse(score);
        Friends = new Dictionary<string, string>();
        Achievements = new List<GameAchievement>();
    }

    public User(User user)
    {
        Name = user.Name;
        Score = user.Score;
        ProfilePic = user.ProfilePic;
        Friends = new Dictionary<string, string>(user.Friends);
//        Achievements = new List<GameAchievement>();
        Achievements = user.Achievements;
    }

    private User()
    {
        Name = "guest";
        Score = 0;
        ProfilePic = null;
        Friends = new Dictionary<string, string>();
        Achievements = new List<GameAchievement>();
    }
}