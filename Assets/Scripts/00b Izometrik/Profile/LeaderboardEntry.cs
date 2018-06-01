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
        ProfilePic.sprite = Util.Texture2Sprite(member.image);
        ProfileName.text = member.userName;
    }
}