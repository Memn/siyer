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
        ProfilePic.sprite = Util.Texture2Sprite(Social.localUser.image);
        Social.LoadAchievements(achievements =>
        {
            var completed = achievements.Count(achievement => achievement.completed);
            Achievements.text = string.Format("{0}/{1}", completed, achievements.Length);
        });

        AchievementsTabs.Init();
        LeaderboardTabs.Init();
    }

    public void Load(Selectable tab)
    {
        if (tab.name.Equals("Rozetler"))
        {
            Util.ClearChildren(AchievementsContainer.transform);
            Social.LoadAchievements(achievements =>
            {
                Util.Load(AchievementsContainer, AchievementsEntryPrefab, achievements, (entry, member) =>
                {
                    var achievementEntry = entry.GetComponent<AchievementEntry>();
                    achievementEntry.Init(member);
                });
            });
        }
        else if (tab.name.Equals("Binalar"))
        {
            Social.ShowAchievementsUI();
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
            Debug.Log("Loading " + tab.name);
        }
    }
}