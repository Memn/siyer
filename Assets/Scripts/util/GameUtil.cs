using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameUtil : MonoBehaviour
{
    private static bool inited;
    private static List<Achievement> _achievements;

    public static List<Achievement> Achievements
    {
        get
        {
            if (!inited)
                Init();
            return _achievements;
        }
    }

    private static void Init()
    {
        // All achievements decleration goes here 
        // can be read from file
        _achievements = new List<Achievement>();
        for (var i = 0; i < 30; i++)
            _achievements.Add(new Achievement(string.Format("{0}", i), 0.0));
    }

    public static User FindUser(KeyValuePair<string, string> friend)
    {
        return new User(friend.Value, "0");
    }
}