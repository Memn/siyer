using System.Collections.Generic;
using System.Data;

public class User
{
    public string _id;
    public string _username;
    public int _score;
    public int _level;
    public IEnumerable<AchievementDto> _achievements;

    public User(string id = "guest", string username = "guest", int score = 0, int level = 1)
    {
        _id = id;
        _username = username;
        _score = score;
        _level = level;
        _achievements = new List<AchievementDto>();
    }
}