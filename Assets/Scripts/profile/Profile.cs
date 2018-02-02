using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;
using UnityEngine.SceneManagement;


public class Profile : MonoBehaviour
{

    public Image ProfilePic;
    public Text ProfileName;
    public Text Score;
    public Text Achievements;

    public Text Leaderboard;
    public GameObject LeaderboardContainer;

    public GameObject LeaderboardEntryPanelPrefab;


    void Awake()
    {
        
        if (FB.IsLoggedIn)
        {
            FacebookManager.Instance.GetProfile();
            FacebookManager.Instance.QueryLeaderboard();
            FB.API("/me/scores", HttpMethod.GET, DisplayScore);
        }

        DisplayProfile();
        DisplayLeaderboard();
    }// Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    void DisplayProfile()
    {
        if (FacebookManager.Instance.ProfileName != null)
        {
            ProfileName.text = FacebookManager.Instance.ProfileName;
        }
        else
        {
            StartCoroutine("WaitForProfileName");
        }
        if (FacebookManager.Instance.ProfilePic != null)
        {
            ProfilePic.sprite = FacebookManager.Instance.ProfilePic;
        }
        else
        {
            StartCoroutine("WaitForProfilePic");
        }
    }

    IEnumerator WaitForProfileName()
    {
        while (FacebookManager.Instance.ProfileName == null)
            yield return null;
        DisplayProfile();
    }

    IEnumerator WaitForProfilePic()
    {
        while (FacebookManager.Instance.ProfilePic == null)
            yield return null;
        DisplayProfile();
    }

    void DisplayLeaderboard()
    {
        if (FacebookManager.Instance.Leaderboard != null)
        {
            foreach (var user in FacebookManager.Instance.Leaderboard)
            {
                GameObject scorePanel = Instantiate(LeaderboardEntryPanelPrefab, Vector3.zero, Quaternion.identity);
                scorePanel.transform.SetParent(LeaderboardContainer.transform);
                scorePanel.GetComponent<ScoreboardHelper>().UserName.text = user.Name;
                scorePanel.GetComponent<ScoreboardHelper>().Score.text = user.Score;
            }
        }
        else
        {
            StartCoroutine("WaitForScores");
        }
    }
    IEnumerator WaitForScores()
    {
        while (FacebookManager.Instance.Leaderboard == null)
            yield return null;
        DisplayLeaderboard();
    }

    void DisplayScore(IResult result)
    {
        if (string.IsNullOrEmpty(result.Error))
        {
            Debug.Log("My score: " + result.RawResult);
            Score.text = result.ResultDictionary["data"].ToString();
        }
        else
        {
            Dictionary<string, string> score = new Dictionary<string, string>();
            score.Add("score", "0");
            FB.API("/me/scores", HttpMethod.POST, CreateScores, score);
            Debug.LogError("Cannot load score");
            Debug.LogError(result.Error);
        }
    }



    void CreateScores(IResult result)
    {

    }

}
