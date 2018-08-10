using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ProfileHelper : MonoBehaviour, LoadableHelper
{
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
        ProfileName.text = Social.localUser.userName;
        ProfilePic.sprite = FindObjectOfType<BadgeManager>().SpriteOf(CommonResources.Levels(UserManager.Game.Level));

        var achievements = UserManager.Game.Achievements.ToArray();
        var completed = achievements.Count(achievement => achievement.completed);
        Achievements.text = string.Format("{0}/{1}", completed, achievements.Length);
        Score.text = UserManager.Game.Score.ToString();

        AchievementsTabs.Init();
        LeaderboardTabs.Init();
    }

    public void Load(Selectable tab)
    {
        if (tab.name.Equals("Rozetler"))
        {
            var badges = UserManager.Game.Badges;
            Util.ClearChildren(AchievementsContainer.transform);
            Util.Load(AchievementsContainer, AchievementsEntryPrefab, badges, (entry, member) =>
            {
                var achievementEntry = entry.GetComponent<AchievementEntry>();
                achievementEntry.Init(member);
            });
        }
        else if (tab.name.Equals("Binalar"))
        {
            var buildings = UserManager.Game.Buildings;
            Util.ClearChildren(AchievementsContainer.transform);
            Util.Load(AchievementsContainer, AchievementsEntryPrefab, buildings, (entry, member) =>
            {
                var achievementEntry = entry.GetComponent<AchievementEntry>();
                achievementEntry.Init(member);
            });
        }
        else if (tab.name.Equals("Friends"))
        {
            Util.ClearChildren(LeaderboardContainer.transform);
            Social.localUser.LoadFriends((successful) =>
            {
                if (successful)
                {
                    Util.Load(LeaderboardContainer, LeaderboardEntryPrefab, Social.localUser.friends,
                              (entry, member) => { entry.GetComponent<LeaderboardEntry>().Init(member); });
                }
            });
        }
        else if (tab.name.Equals("General"))
        {
            Util.ClearChildren(LeaderboardContainer.transform);
            Social.ShowLeaderboardUI();
        }
        else
        {
            LogUtil.Log("Loading " + tab.name);
        }
    }
}