using System.Collections.Generic;
using UnityEngine.SocialPlatforms;

namespace managers
{
    public interface IAchievementsManager
    {
        IEnumerable<AchievementDto> Buildings { get; }
        IEnumerable<AchievementDto> Achievements { get; }
        IEnumerable<AchievementDto> Badges { get; }
        IEnumerable<KeyValuePair<bool, CommonResources.Duty>> CurrentLevelAchievementCompletions { get; }
        IAchievement AchievementOf(string buildingId);
        bool IsAchieved(string reward);
        void CheckLocks();
        void LocalUnlock(AchievementDto achievement);
        string RewardOf(CommonResources.Building building);
    }
}