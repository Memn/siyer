using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class LeaderboardEntry : MonoBehaviour
{
    public Text ProfileName;
    public GameObject Scoring;
    public Text Score;

    public void Init(dreamloLeaderBoard.Score member)
    {
        ProfileName.text = member.playerName;
        Score.text = member.score.ToString();
    }

    public void Init(string message)
    {
        ProfileName.text = message;
        Scoring.SetActive(false);
    }
}