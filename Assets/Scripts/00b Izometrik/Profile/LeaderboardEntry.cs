using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardEntry : MonoBehaviour
{

	public Image ProfilePic;
	public Text ProfileName;
	public Text Score;
	public Text Achievements;

	public void InitProfile(User user)
	{
		ProfilePic.sprite = user.ProfilePic;
		ProfileName.text = user.Name;
		Score.text = user.Score.ToString();
		Achievements.text = string.Format("{0}/{1}", user.Achievements.Count, GameUtil.Achievements.Count);
	}
	
}
