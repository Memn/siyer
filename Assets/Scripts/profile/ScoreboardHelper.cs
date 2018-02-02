using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreboardHelper : MonoBehaviour
{

    public Text UserName
    {
        get
        {
            return transform.Find("UserInfo").Find("User Name").GetComponent<Text>();
        }
    }

    public Text Score
    {
        get
        {
            return transform.Find("UserInfo")
            .Find("ScoresAndAchievements")
            .Find("ScoreText").GetComponent<Text>();
        }
    }
}
