using UnityEngine;
using UnityEngine.UI;

public class ProfileHelper : MonoBehaviour, LoadableHelper
{
    private User _user;

    public Image ProfilePic;
    public Text ProfileName;
    public Text Score;
    public Text Achievements;

    public MenuTabsHandler AchievementsTabs;
    public MenuTabsHandler LeaderboardTabs;

//    public Text Leaderboard;
    public GameObject LeaderboardContainer;
    public GameObject LeaderboardEntryPrefab;

    public GameObject AchievementsContainer;
    public GameObject AchievementsEntryPrefab;


    private void Awake()
    {
        LoadUser();
    }

    public void LoadUser()
    {
        _user = UserManager.Instance.User;
        ProfileName.text = _user.Name;
        ProfilePic.sprite = _user.ProfilePic;
        Score.text = _user.Score.ToString();
        Achievements.text = string.Format("{0}/{1}", _user.Achievements.Count, GameUtil.Achievements.Count);
        AchievementsTabs.Init();
        LeaderboardTabs.Init();
    }

    public void Load(Selectable tab)
    {
        if (tab.name.Equals("Rozetler"))
        {
//            Util.ClearChildren(AchievementsContainer.transform);
            LoadBadges();
        }
        else if (tab.name.Equals("Binalar"))
        {
            //            Util.ClearChildren(AchievementsContainer.transform);
            //            LoadBuildings();
        }
        else if (tab.name.Equals("Friends"))
        {
            Util.ClearChildren(LeaderboardContainer.transform);
            LoadFriends();
        }
        else if (tab.name.Equals("General"))
        {
            Util.ClearChildren(LeaderboardContainer.transform);
        }

        else
        {
            Debug.Log("Loading " + tab.name);
        }
    }

    private void LoadBadges()
    {
//        Util.Load(AchievementsContainer, AchievementsEntryPrefab, _user.Achievements, (entry, member) =>
//        {
//            // TODO: Update Achievements to show
//        });
    }

    private void LoadFriends()
    {
        Util.Load(LeaderboardContainer, LeaderboardEntryPrefab, _user.Friends, (entry, member) =>
        {
            var leaderboardEntry = entry.GetComponent<LeaderboardEntry>();
            leaderboardEntry.InitProfile(GameUtil.FindUser(member));
        });
    }
}