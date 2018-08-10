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
    public Image Completion;
    public Sprite Completed;
    public Sprite NotCompleted;

    public void Init(IAchievement achievement)
    {
        var badgeManager = FindObjectOfType<BadgeManager>();
        var sprite = AchievementPic.sprite = badgeManager.SpriteOf(achievement.id);
        if (sprite == null)
        {
            Destroy(gameObject);
            return;
        }

        AchievementName.text = badgeManager.TitleOf(achievement.id);
        if (achievement.completed)
        {
            Completion.sprite = Completed;
            Completion.color = Color.green;
        }
        else
        {
            Completion.sprite = NotCompleted;
            Completion.color = Color.red;
        }

        if (achievement.completed)
        {
            AchievementPic.color = Color.white;
        }
#if UNITY_ANDROID
        var games = achievement as PlayGamesAchievement;
        if (games == null) return;
        AchievementPic.sprite = Util.Texture2Sprite(games.image);
        AchievementName.text = games.title;
#endif
    }
}