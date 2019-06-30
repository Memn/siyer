using System.Linq;
using UnityEngine;

namespace managers
{
    public class ProgressManager : MonoBehaviour, IProgressManager
    {
        public static IProgressManager Instance
        {
            get { return UserManagement.Instance.GetComponent<ProgressManager>(); }
        }


        public void UnlockAchievement(string id, int score)
        {
            var achievement = AchievementsManager.Instance.AchievementOf(id) as AchievementDto;
            Debug.Assert(achievement != null, "achievement is null");
            if (achievement.completed) return;
            achievement.percentCompleted = 100;
            LogUtil.Log(id + " unlocking...");
#if UNITY_EDITOR
            ReportProgress(achievement, score);
#else
        achievement.ReportProgress(success =>
        {
            if (success)
                ReportProgress(achievement, score);
            LogUtil.Log(id + " unlocked successfully or not: " + success);
        });
#endif
        }

        private void ReportProgress(AchievementDto achievement, int score)
        {
            AchievementsManager.Instance.LocalUnlock(achievement);
            ReportScore(score);
            CheckLevelUp(true);
        }

        public void CheckLevelUp(bool save)
        {
            if (!AchievementsManager.Instance.CurrentLevelAchievementCompletions.All(achievementCompletions =>
                achievementCompletions.Key)) return;
            LevelUp();
            if (save)
                UserManagement.Instance.Save();
        }

        private void LevelUp()
        {
            if (ScoreManager.Instance.Level == 5) return;
            ScoreManager.Instance.Level++;
            UnlockAchievement(CommonResources.Levels(ScoreManager.Instance.Level), 250);
            UnlockAchievement(CommonResources.Stories(ScoreManager.Instance.Level), 50);
            Invoke("CheckLocks", 0.8f);
        }

        public void CheckLocks()
        {
            if (FindObjectOfType<BuildingManager>())
                FindObjectOfType<BuildingManager>().LockingAdjustments();
        }

        public void Reward(CommonResources.Building building, int score)
        {
            var reward = AchievementsManager.Instance.RewardOf(building);
            Debug.Assert(reward != null, "reward != null");
            Instance.UnlockAchievement(reward, score);
        }

        public void ReportScore(int score)
        {
            ScoreManager.Instance.UpdateScore(score);
        }

    }
}