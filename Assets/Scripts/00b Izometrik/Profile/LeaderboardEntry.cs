using GooglePlayGames;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class LeaderboardEntry : MonoBehaviour
{
    public Image ProfilePic;
    public Text ProfileName;
    public Text Score;
    public Text Achievements;

    public void Init(IUserProfile member)
    {
        
        var user = member as PlayGamesLocalUser;
        if (user == null) return;
        StartCoroutine(user.LoadImage());
        ProfilePic.sprite = Util.Texture2Sprite(user.image);
        ProfileName.text = user.userName;
        
    }
}