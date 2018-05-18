using System;
using System.Collections.Generic;

[Serializable]
public class RawGame
{
    public string UserName = "";
    public int Score = 0;
    public string ProfilePic;
    public List<string> Achievements;
    public long SaveTime;
    public long PlayedTime;


    public RawGame(Game game)
    {
        UserName = game.UserName;
        Score = game.Score;
        ProfilePic = Util.Sprite2Str(game.ProfilePic);
        Achievements = new List<string>();
        foreach (var pair in game.Achievements) Achievements.Add(pair.Key + "|" + pair.Value);
        SaveTime = game.SaveTime.ToBinary();
        PlayedTime = game.PlayedTime.Ticks;
    }

    public Game ToGame()
    {
        var game = new Game
        {
            UserName = UserName,
            Score = Score,
            ProfilePic = Util.Str2Sprite(ProfilePic),
            SaveTime = DateTime.FromBinary(SaveTime),
            PlayedTime = TimeSpan.FromTicks(PlayedTime)
        };
        Achievements.ForEach(s =>
        {
            var split = s.Split('|');
            game.Achievements.Add(split[0], bool.Parse(split[1]));
        });
        return game;
    }
}