using System;
using UnityEngine.SocialPlatforms;

[Serializable]
public class GameAchievement : IAchievement
{
    private string _id;
    private double _percentCompleted;
    private bool _completed;
    private bool _hidden;
    private DateTime _lastReportedDate;

    public GameAchievement FromJson(string from)
    {
        return null;
    }

    public void ReportProgress(Action<bool> callback)
    {
    }

    string IAchievement.id
    {
        get { return _id; }
        set { _id = value; }
    }

    double IAchievement.percentCompleted
    {
        get { return _percentCompleted; }
        set { _percentCompleted = value; }
    }

    bool IAchievement.completed
    {
        get { return _completed; }
    }

    bool IAchievement.hidden
    {
        get { return _hidden; }
    }

    DateTime IAchievement.lastReportedDate
    {
        get { return _lastReportedDate; }
    }
}