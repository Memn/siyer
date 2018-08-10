using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Game
{
    // For persistence of the game 
    // a copy of user object
    internal GameData _gameData;

    // User
    public string UserName
    {
        get { return _gameData.UserName; }
        set { _gameData.UserName = value; }
    }

    public int Score
    {
        get { return _gameData.Score; }
        set { _gameData.Score = value; }
    }

    public int Level
    {
        get { return _gameData.Level; }
        set { _gameData.Level = value; }
    }

    // Achievements
    public IEnumerable<AchievementDto> Achievements
    {
        // ReSharper disable once CoVariantArrayConversion
        get { return _gameData.Achievements; }
    }

    public IEnumerable<AchievementDto> Badges
    {
        get { return _gameData.Achievements.FindAll(dto => CommonResources.IsBadge(dto.id)); }
    }

    public IEnumerable<AchievementDto> Buildings
    {
        get { return _gameData.Achievements.FindAll(dto => CommonResources.IsBuilding(dto.id)); }
    }

    public IEnumerable<KeyValuePair<bool, string>> CurrentLevelAchievementCompletions
    {
        get
        {
            var duties = CommonResources.DutyOf(Level);
            return duties.Select(duty => new KeyValuePair<bool, string>(IsAchieved(duty.Reward), duty.Title));
        }
    }

    public IAchievement AchievementOf(string id)
    {
        return Achievements.FirstOrDefault(achievement => achievement.id == id);
    }

    public bool IsAchieved(string id)
    {
        var achievement = Achievements.FirstOrDefault(ac => ac.id == id);
        return achievement != null && achievement.completed;
    }

    public string RewardOf(CommonResources.Building building)
    {
        return CommonResources.DutyOf(Level).Find(duty => duty.Building == building).Reward;
    }
}