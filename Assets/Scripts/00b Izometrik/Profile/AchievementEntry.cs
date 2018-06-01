﻿using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class AchievementEntry : MonoBehaviour
{
    public Image AchievementPic;
    public Text AchievementName;
    public Text Completion;

    public void Init(IAchievement playGamesAchievement)
    {
//        var achievement = playGamesAchievement as PlayGamesAchievement;
        var achievement = UserManager.Game.DescriptionOf(playGamesAchievement.id);
        
        if (achievement == null) return;

        AchievementPic.sprite = Util.Texture2Sprite(achievement.image);
        AchievementName.text = achievement.title;
        Completion.text = "True";
    }
}