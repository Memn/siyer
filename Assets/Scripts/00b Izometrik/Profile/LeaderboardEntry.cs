using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class LeaderboardEntry : MonoBehaviour
{
    public Text ProfileName;
    public GameObject Scoring;
    public Text Score;

    public void Init(User member)
    {
        ProfileName.text = member.Username;
        Score.text = member.Score.ToString();
    }

    public void Init(string message)
    {
        ProfileName.text = message;
        Scoring.SetActive(false);
    }
}