using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Game
{
    // For persistence of the game 
    // a copy of user object

    private GameData _gameData = new GameData();

    // User
    public string UserName
    {
        get { return _gameData.UserName; }
        set { _gameData.UserName = value; }
    }

    public int Score
    {
        get { return _gameData.Score; }
    }

    public Sprite ProfilePic
    {
        get { return Util.Texture2Sprite(_gameData.ProfilePic); }
        set { _gameData.ProfilePic = value.texture; }
    }

    public TimeSpan PlayedTime
    {
        get { return TimeSpan.FromTicks(_gameData.PlayedTime); }
        set { _gameData.PlayedTime = value.Ticks; }
    }

    public int Level
    {
        get { return _gameData.Level; }
        set { _gameData.Level = value; }
    }

    // Achievements
    public IAchievement[] Achievements
    {
        // ReSharper disable once CoVariantArrayConversion
        get { return _gameData.Achievements.ToArray(); }
    }

    public void Save()
    {
        var ds = new DataContractSerializer(typeof(GameData));
        var file = File.Open(Util.SaveFilePath, FileMode.OpenOrCreate);
        var writer = XmlDictionaryWriter.CreateTextWriter(file);
        ds.WriteObject(writer, _gameData);
        writer.Close();
        file.Close();
    }

    public static Game Load()
    {
        Debug.Log(("Checking " + Util.SaveFilePath + " for saves."));
        if (!File.Exists(Util.SaveFilePath)) return new Game();

        var file = File.Open(Util.SaveFilePath, FileMode.Open);
        var reader = XmlDictionaryReader.CreateTextReader(file, new XmlDictionaryReaderQuotas());

        var ds = new DataContractSerializer(typeof(GameData));
        Debug.Log("Loading game from " + Util.SaveFilePath);
        var gameData = ds.ReadObject(reader) as GameData;
        reader.Close();
        file.Close();
        Debug.Log(gameData == null ? "Game loading failed" : "Game Loaded Successfully");
        return gameData == null ? new Game() : new Game {_gameData = gameData};
    }

    public void UnlockedAchievement(IAchievement achievement)
    {
        var local = Achievements.FirstOrDefault(ach => ach.id == achievement.id);
        if (local != null) return;
        var dto = new AchievementDto(achievement) {completed = true};
        _gameData.Achievements.Add(dto);
        Save();
    }
    public void Sync(IEnumerable<IAchievement> achievements)
    {
        var toBeSaved = false;
        var temp = new List<AchievementDto>();

        foreach (var platform in achievements)
        {
            var local = Achievements.FirstOrDefault(ach => ach.id == platform.id);
            if (local == null)
            {
                var dto = new AchievementDto(platform);
                temp.Add(dto);
                toBeSaved = true;
            }
            else
            {
                if (local.completed)
                {
                    UserManager.Instance.UnlockAchievement(local.id);
                }
            }
        }


        if (!toBeSaved) return;
        _gameData.Achievements.AddRange(temp);
    }

    public void Sync(ILocalUser localUser)
    {
        UserName = localUser.userName;
        if (localUser.image == null)
        {
            UserManager.SyncUserLater(2);
        }
        else
        {
            ProfilePic = Util.Texture2Sprite(localUser.image);
        }
    }


    public IEnumerable<CommonResources.Duty> LevelDuties
    {
        get
        {
            var value = CommonResources.DutyOf(Level);
            return value ?? new List<CommonResources.Duty>();
        }
    }

    public IAchievement AchievementOf(string id)
    {
        return Achievements.FirstOrDefault(achievement => achievement.id == id);
    }

    public bool IsAchieved(string id)
    {
        var achievement = Achievements.FirstOrDefault(ac => ac.id == id);
        return achievement != null && achievement.completed;
    }
}