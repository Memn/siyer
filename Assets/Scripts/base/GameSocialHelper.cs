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

    private static bool _save = false;

    public static void SyncUser(Game game)
    {
        if (game.UserName == "")
        {
            game.UserName = Social.localUser.userName;
            _save = true;
        }

        if (game.UserName != Social.localUser.userName)
        {
            // user is logged in & loaded data does not belong to him.
            // save old user
            GameSaveLoadHelper.Save(game, game.UserName + ".data");
            // clear cache
            GameSaveLoadHelper.ClearCache();
            // if user exists loads user data, if not loads Game from scratch.
            game = GameSaveLoadHelper.Load(game.UserName + ".data");
            _save = true;
        }

        Social.LoadAchievements(achievements => SyncAchievements(game, achievements));
        SyncScores(game);
        if (_save)
            GameSaveLoadHelper.Save(game);

    }

    private static void SyncScores(Game game)
    {
        LogUtil.Log("Current score is:" + game.Score);
        UserManager.LoadMyScore(myScore =>
        {
            var score = myScore.score;
            if (game.Score == score)
            {
                LogUtil.Log("Scores are already sync.");
                return;
            }

            if (game.Score > score)
            {
                LogUtil.Log("Scores are not sync updating score of " + game.UserName + " with score: " + game.Score);
                UserManager.UpdateScore();
            }
            else
            {
                LogUtil.Log("Local seems behind. Sync " + game.UserName + "'s score: " + game.Score + " with server.");
                game.Score = score;
                _save = true;
            }
        });
        LogUtil.Log("Score sync is completed..");
    }

    private static void SyncAchievements(Game game, ICollection<IAchievement> achievements)
    {
        if (achievements.Count == 0)
            LogUtil.Log("Error: no achievements found");
        else
        {
            foreach (var platform in achievements)
            {
                var id = platform.id;
                var local = game.Achievements.FirstOrDefault(ach => ach.id == id);
                if (local == null)
                {
                    LogUtil.Log("Not found in local ignoring: " + id);
                    continue;
                }

                if (local.completed == platform.completed) continue;

                if (local.completed && !platform.completed)
                {
                    LogUtil.Log("Updating platform: " + id);
                    platform.percentCompleted = 100;
                    platform.ReportProgress(success => LogUtil.Log(id + " unlocked successfully or not: " + success));
                }
                else if (!local.completed && platform.completed)
                {
                    LogUtil.Log("Updating local: " + local.id);
                    LocalUnlock(game, local, false);
                    _save = true;
                    // Also need to check level updates!!
                    UserManager.CheckLevelUp(false);
                }
            }

        }

        LogUtil.Log("Achievements sync is completed..");
    }

    public static void LocalUnlock(Game game, AchievementDto achievement, bool save = true)
    {
        LogUtil.Log("Local unlock: " + achievement.id);
        achievement.completed = true;
        achievement.percentCompleted = 100;
        if (save)
            GameSaveLoadHelper.Save(game);
    }
}