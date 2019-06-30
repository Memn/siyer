using System.Collections.Generic;
using System.Linq;
using DataBank;
using UnityEngine;

public class User
{
    public string Id;
    public string Username;
    public int Score;
    public int Level;
    public List<AchievementDto> Achievements;

    public User(string id = "guest", string username = "guest", int score = 0, string achievements = "", int level = 1)
    {
        Id = id;
        Username = username;
        Score = score;
        Level = level;
        Achievements = string.IsNullOrEmpty(achievements)
            ? InitAllAchievements()
            : JsonHelper.FromJson<AchievementDto>(achievements).ToList();
    }

    private static List<AchievementDto> InitAllAchievements()
    {
        return CommonResources.AllAchievements().Select(id =>
        {
            var achievement = Social.CreateAchievement();
            achievement.id = id;
            achievement.percentCompleted = 0;
            return new AchievementDto(achievement);
        }).ToList();
    }

    public string DbAchievements
    {
        get { return JsonHelper.ToJson(Achievements.ToArray()); }
        set { Achievements = JsonHelper.FromJson<AchievementDto>(value).ToList(); }
    }

    public IEnumerable<AchievementDto> Badges
    {
        get { return Achievements.FindAll(dto => CommonResources.IsBadge(dto.id)); }
    }

    public IEnumerable<AchievementDto> Buildings
    {
        get { return Achievements.FindAll(dto => CommonResources.IsBuilding(dto.id)); }
    }
}