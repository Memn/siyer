using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace managers
{
    public class AchievementsManager : MonoBehaviour, IAchievementsManager
    {
        public static IAchievementsManager Instance
        {
            get { return UserManagement.Instance.GetComponent<AchievementsManager>(); }
        }

        public IEnumerable<AchievementDto> Buildings
        {
            get { return UserManagement.Instance.User.Buildings; }
        }

        public IEnumerable<AchievementDto> Achievements
        {
            get { return UserManagement.Instance.User.Achievements; }
        }

        public IEnumerable<AchievementDto> Badges
        {
            get { return UserManagement.Instance.User.Badges; }
        }

        public IEnumerable<KeyValuePair<bool, CommonResources.Duty>> CurrentLevelAchievementCompletions
        {
            get
            {
                var duties = CommonResources.DutyOf(UserManagement.Instance.User.Level);
                return duties.Select(
                    duty => new KeyValuePair<bool, CommonResources.Duty>(IsAchieved(duty.Reward), duty));
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

        public void CheckLocks()
        {
            if (FindObjectOfType<BuildingManager>())
                FindObjectOfType<BuildingManager>().LockingAdjustments();
        }

        public void LocalUnlock(AchievementDto achievement)
        {
            LogUtil.Log("Local unlock: " + achievement.id);
            achievement.completed = true;
            achievement.percentCompleted = 100;
            UserManagement.Instance.Save();
        }

        public string RewardOf(CommonResources.Building building)
        {
            return CommonResources.DutyOf(ScoreManager.Instance.Level).Find(duty => duty.Building == building).Reward;
        }
    }
}