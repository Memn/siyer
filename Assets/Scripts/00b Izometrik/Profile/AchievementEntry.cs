#if UNITY_ANDROID
using UnityEngine.UI;
using UnityEngine;
using GooglePlayGames;
#endif
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class AchievementEntry : MonoBehaviour
{
    public Image AchievementPic;
    public Text AchievementName;
    public Text Completion;

    public void Init(IAchievement playGamesAchievement)
    {
#if UNITY_ANDROID

        var achievement = playGamesAchievement as PlayGamesAchievement;
        if (achievement == null) return;
        AchievementPic.sprite = Util.Texture2Sprite(achievement.image);
        AchievementName.text = achievement.title;
        Completion.text = "True";
#endif
    }
}