using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RawUser : MonoBehaviour
{
    public string FacebookID;
    public string Name;
    public int Score;
    public string ProfilePic;
    public List<string> Friends = new List<string>();
    public List<string> Achievements = new List<string>();

    public RawUser(User user)
    {
        FacebookID = user.FacebookID;
        Name = user.Name;
        Score = user.Score;
        ProfilePic = Util.Sprite2Str(user.ProfilePic);
        foreach (var friend in user.Friends)
        {
            Friends.Add(friend.Key + "-" + friend.Value);
        }

        foreach (var achievement in user.Achievements)
        {
//            Achievements.Add();
        }
    }

    public User ToUser()
    {
        var fList = new Dictionary<string, string>();
        foreach (var friend in Friends)
        {
            var split = friend.Split('-');
            fList.Add(split[0], split[1]);
        }

        return new User(Name, Score.ToString())
        {
            FacebookID = FacebookID,
//            Achievements = Achievements,
            Friends = fList,
            ProfilePic = Util.Str2Sprite(ProfilePic)
        };
    }
}