using System;
using UnityEngine;
using UnityEngine.SocialPlatforms;

[Serializable]
public class AchievementDto : IAchievement
{
    [SerializeField] private string _id;
    [SerializeField] private double _percentCompleted;
    [SerializeField] private bool _completed;
    [SerializeField] private bool _hidden;
    [SerializeField] private DateTime _lastReportedDate;

    internal AchievementDto(IAchievement achievement)
    {
        id = achievement.id;
        percentCompleted = achievement.percentCompleted;
        completed = achievement.completed;
        hidden = achievement.hidden;
        lastReportedDate = achievement.lastReportedDate;
    }


    public void ReportProgress(Action<bool> callback)
    {
        Social.ReportProgress(_id, _percentCompleted, callback);
    }

    public string id
    {
        get { return _id; }
        set { _id = value; }
    }

    public double percentCompleted
    {
        get { return _percentCompleted; }
        set { _percentCompleted = value; }
    }

    public bool completed
    {
        get { return _completed; }
        set { _completed = value; }
    }

    public bool hidden
    {
        get { return _hidden; }
        private set { _hidden = value; }
    }

    public DateTime lastReportedDate
    {
        get { return _lastReportedDate; }
        private set { _lastReportedDate = value; }
    }
}