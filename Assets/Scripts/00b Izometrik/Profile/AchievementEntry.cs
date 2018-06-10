#if UNITY_ANDROID
using GooglePlayGames;
#endif
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class AchievementEntry : MonoBehaviour
{
    public Image AchievementPic;
    public Text AchievementName;
    public Text Completion;

    public void Init(IAchievement playGamesAchievement)
    {
        var badgeManager = FindObjectOfType<BadgeManager>();
        AchievementPic.sprite = badgeManager.SrpiteOf(playGamesAchievement.id);
        AchievementName.text = badgeManager.TitleOf(playGamesAchievement.id);
        Completion.text = playGamesAchievement.completed.ToString();
        if (playGamesAchievement.completed)
        {
            AchievementPic.color = Color.white;
        }
#if UNITY_ANDROID
        var games = playGamesAchievement as PlayGamesAchievement;
        if (games == null) return;
        AchievementPic.sprite = Util.Texture2Sprite(games.image);
        AchievementName.text = games.title;
#endif
    }
}