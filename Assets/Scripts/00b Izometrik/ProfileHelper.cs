using UnityEngine;
using UnityEngine.UI;

public class ProfileHelper : MonoBehaviour
{
    private User _user;

    public Image ProfilePic;
    public Text ProfileName;
    public Text Score;
    public Text Achievements;

    public Text Leaderboard;
    public GameObject LeaderboardContainer;

    public GameObject LeaderboardEntryPanelPrefab;


    private void Awake()
    {
        _user = UserManager.Instance.User;
        ProfileName.text = _user.Name;
        ProfilePic.sprite = _user.ProfilePic;
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