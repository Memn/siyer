using System.Collections.Generic;
using System.Linq;
using managers;
using UnityEngine;
using UnityEngine.SocialPlatforms;
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
        ProfilePic.sprite = FindObjectOfType<BadgeManager>().SpriteOf(CommonResources.Levels(ScoreManager.Instance.Level));

        var achievements = AchievementsManager.Instance.Achievements.ToArray();
        var completed = achievements.Count(achievement => achievement.completed);
        Achievements.text = string.Format("{0}/{1}", completed, achievements.Length);
        Score.text = ScoreManager.Instance.Score.ToString();

        AchievementsTabs.Init();
        LeaderboardTabs.Init();
    }

    private enum LeaderboardStates
    {
        None,
        Friends,
        General
    }

    private LeaderboardStates _current = LeaderboardStates.None;

    public void Load(Selectable tab)
    {
        if (tab.name.Equals("Rozetler"))
        {
            var badges = AchievementsManager.Instance.Badges;
            Util.ClearChildren(AchievementsContainer.transform);
            Util.Load(AchievementsContainer, AchievementsEntryPrefab, badges, (entry, member) =>
            {
                var achievementEntry = entry.GetComponent<AchievementEntry>();
                achievementEntry.Init(member);
            });
        }
        else if (tab.name.Equals("Binalar"))
        {
            var buildings = AchievementsManager.Instance.Buildings;
            Util.ClearChildren(AchievementsContainer.transform);
            Util.Load(AchievementsContainer, AchievementsEntryPrefab, buildings, (entry, member) =>
            {
                var achievementEntry = entry.GetComponent<AchievementEntry>();
                achievementEntry.Init(member);
            });
        }
        else if (tab.name.Equals("Friends"))
        {
            if (_current == LeaderboardStates.Friends)
                return;
            _current = LeaderboardStates.Friends;
            Social.localUser.LoadFriends(successful =>
            {
                if (successful)
                    LoadFriends(Social.localUser.friends);
            });
        }
        else if (tab.name.Equals("General"))
        {
            if (_current == LeaderboardStates.General)
                return;
            _current = LeaderboardStates.General;
            LogUtil.Log("Loading all scores.");
        }
        else
        {
            LogUtil.Log("Loading " + tab.name);
        }
    }

    private void LoadFriends(IEnumerable<IUserProfile> localUserFriends)
    {
        LogUtil.Log("Loading friend scores.");
    }


    private void LoadLeaderboard(IEnumerable<User> scores)
    {
        Util.ClearChildren(LeaderboardContainer.transform);
        if (!scores.Any())
        {
            Util.LoadSingle(LeaderboardContainer, LeaderboardEntryPrefab, "Gösterilecek kişi bulunamadı",
                (entry, member) => entry.GetComponent<LeaderboardEntry>().Init(member));
        }
        else
            Util.Load(LeaderboardContainer, LeaderboardEntryPrefab, scores, (entry, member) =>
            {
                var leaderboardEntry = entry.GetComponent<LeaderboardEntry>();
                leaderboardEntry.Init(member);
            });
    }
}