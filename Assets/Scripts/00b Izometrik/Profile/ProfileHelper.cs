using UnityEngine;
using UnityEngine.UI;

public class ProfileHelper : MonoBehaviour
{
    private User _user;

    public Image ProfilePic;
    public Text ProfileName;
    public Text Score;
    public Text Achievements;

//    public Text Leaderboard;
    public GameObject LeaderboardContainer;
    public GameObject LeaderboardEntryPrefab;


    private void Awake()
    {
        _user = UserManager.Instance.User;
        ProfileName.text = _user.Name;
        ProfilePic.sprite = _user.ProfilePic;
        Score.text = _user.Score.ToString();
        Achievements.text = string.Format("{0}/{1}", _user.Achievements.Count, GameUtil.Achievements.Count);
        foreach (var friend in _user.Friends)
        {
            var friendObj = Instantiate(LeaderboardEntryPrefab, Vector3.zero, Quaternion.identity);
            var friendEntry = friendObj.GetComponent<LeaderboardEntry>();
            friendEntry.InitProfile(GameUtil.FindUser(friend));
            friendObj.transform.SetParent(LeaderboardContainer.transform);
            friendObj.transform.localScale = Vector3.one;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManagementUtil.Load(SceneManagementUtil.Scenes.Izometrik);
        }
    }

    public void ReloadProfile()
    {
        _user = UserManager.Instance.User;
        ProfileName.text = _user.Name;
        ProfilePic.sprite = _user.ProfilePic;
    }
}