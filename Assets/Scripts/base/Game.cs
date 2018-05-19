using System;
using System.Collections.Generic;
using UnityEngine;

public class Game
{
    // For persistence of the game 
    // a copy of user object

    public string UserName = "";
    public int Score = 0;
    public Sprite ProfilePic;
    public Dictionary<string, bool> Achievements = new Dictionary<string, bool>();
    public DateTime SaveTime = DateTime.Now;
    public TimeSpan PlayedTime = TimeSpan.Zero;

    public Game()
    {
        Achievements.Add(SiyerResources.achievement_achievementtest1, true);
        Achievements.Add(SiyerResources.achievement_achievementtest2, false);
        Achievements.Add(SiyerResources.achievement_achievementtest3, false);
        Achievements.Add(SiyerResources.achievement_test4, false);
        Achievements.Add(SiyerResources.achievement_test5, false);
    }

    public static Game Default
    {
        get { return new Game(); }
    }
}