using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class GameSocialHelper
{

    public static void ReportScore(Game game, int score)
    {
        LogUtil.Log(string.Format("Updating Score: {0} to {1}", game.Score, game.Score + score));
        game.Score += score;
        GameSaveLoadHelper.Save(game);
    }

    public static void SyncUser(Game game, bool success)
    {
        LogUtil.Log(!success
            ? "Failed to authenticate, continuing with local save."
            : "Authenticated, checking achievements to sync with local");

        if (!success) return;
        Social.LoadAchievements(achievements => SyncAchievements(game, achievements));
        Social.LoadScores(SiyerResources.leaderboard_genel, scores => SyncScores(game, scores));
    }

    private static void SyncAchievements(Game game, ICollection<IAchievement> achievements)
    {
        if (achievements.Count == 0)
            LogUtil.Log("Error: no achievements found");
        else
        {
            var save = false;
            foreach (var platform in achievements)
            {
                var local = game.Achievements.FirstOrDefault(ach => ach.id == platform.id) as AchievementDto;
                if (local == null)
                {
                    LogUtil.Log("Not found in local ignoring: " + platform.id);
                    continue;
                }

                if (local.completed == platform.completed)
                {
                    LogUtil.Log(string.Format("Local and platform are sync already!: {0} - {1}", local.id,
                        local.completed));
                    continue;
                }

                if (local.completed && !platform.completed)
                {
                    LogUtil.Log("Updating platform: " + platform.id);
                    platform.percentCompleted = 100;
                    var id = platform.id;
                    platform.ReportProgress(success => LogUtil.Log(id + " unlocked successfully or not: " + success));
                }
                else if (!local.completed && platform.completed)
                {
                    LogUtil.Log("Updating local: " + local.id);
                    local.percentCompleted = 100;
                    local.completed = true;
                    save = true;
                    // Also need to check level updates!!
                    UserManager.CheckLevelUp(false);
                }
            }

            if (save)
                GameSaveLoadHelper.Save(game);
        }

        LogUtil.Log("Achievements sync is completed..");
    }

    private static void SyncScores(Game game, ICollection<IScore> scores)
    {
        if (scores == null)
        {
            LogUtil.Error("Scores are null!");
            return;
        }

        if (scores.Count <= 0)
        {
            LogUtil.Log("No Score is found");
        }
        else
        {
            LogUtil.Log("Current score is:" + game.Score);
            var topScore = scores.Sum(score => score.value);
            LogUtil.Log("sum of found score is: " + topScore);
            if (game.Score == topScore)
            {
                LogUtil.Log("Scores are already sync.");
                return;
            }
            if (game.Score > topScore)
            {
#if !UNITY_EDITOR
                var score = (int) (game.Score - topScore);
                Social.ReportScore(score, SiyerResources.leaderboard_genel, scoreSuccess =>
                {
                    if (!scoreSuccess)
                        LogUtil.Log("Error, when reporting score!");
                });
#endif
            }
            else
            {
                game.Score = (int) topScore;
                GameSaveLoadHelper.Save(game);
            }
        }

        LogUtil.Log("Score sync is completed..");
    }

    public static void Unlock(Game game, AchievementDto achievement)
    {
        LogUtil.Log("Local unlock: " + achievement.id);
        var local = game.Achievements.FirstOrDefault(ach => ach.id == achievement.id) as AchievementDto;
        System.Diagnostics.Debug.Assert(local != null, "local != null");
        LogUtil.Log("found local: " + local.id);
        local.completed = true;
        local.percentCompleted = 100;
        GameSaveLoadHelper.Save(game);
    }
}