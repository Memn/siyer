using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using UnityEngine;
using UnityEngine.UI;

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
            SceneManagementUtil.Load(SceneManagementUtil.Scenes.MainMenu);
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
        string score = "0";
        if (string.IsNullOrEmpty(result.Error))
        {
            List<object> deserializedScore = Util.DeserializeScores(result.RawResult);
            if (deserializedScore.Count == 1)
            {
                var entry = (Dictionary<string, object>)deserializedScore[0];
                score = entry["score"].ToString();
            }
        }
        Score.text = score;
    }

    public void SetScore()
    {
        var scoreData = new Dictionary<string, string>();
        scoreData["score"] = Random.Range(10, 200).ToString();
        FB.API("/me/scores", HttpMethod.POST, result =>
        {
            Debug.Log("Score submit result: " + result.RawResult);
        }, scoreData);
    }

}
