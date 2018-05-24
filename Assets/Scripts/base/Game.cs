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

    // Achievements
    public IAchievement[] Achievements
    {
        // ReSharper disable once CoVariantArrayConversion
        get { return _gameData.Achievements.ToArray(); }
    }

    public IAchievementDescription[] AchievementDescriptions
    {
        // ReSharper disable once CoVariantArrayConversion
        get { return _gameData.AchievementDescriptions.ToArray(); }
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
        Debug.Log(string.Format("Checking {0} for saves.", Util.SaveFilePath));
        if (!File.Exists(Util.SaveFilePath)) return new Game();

        var file = File.Open(Util.SaveFilePath, FileMode.Open);
        var reader = XmlDictionaryReader.CreateTextReader(file, new XmlDictionaryReaderQuotas());

        var ds = new DataContractSerializer(typeof(GameData));
        Debug.Log(string.Format("Loading game from {0}", Util.SaveFilePath));
        var gameData = ds.ReadObject(reader) as GameData;
        reader.Close();
        file.Close();
        return gameData == null ? new Game() : new Game {_gameData = gameData};
    }

    [DataContract(Name = "GameData")]
    private class GameData
    {
        [DataMember(Name = "userName")] public string UserName = "";
        [DataMember(Name = "score")] public int Score = 0;
        [DataMember(Name = "profilePic")] public Texture2D ProfilePic;
        [DataMember(Name = "playedTime")] public long PlayedTime = 0;
        [DataMember(Name = "achievements")] public List<AchievementDto> Achievements = new List<AchievementDto>();

        [DataMember(Name = "achievementDescriptions")]
        public List<AchievementDescriptionsDto> AchievementDescriptions = new List<AchievementDescriptionsDto>();
    }

    [DataContract(Name = "Achievements")]
    private class AchievementDto : IAchievement
    {
        [DataMember(Name = "id")] private string _id;

        [DataMember(Name = "percentCompleted")]
        private double _percentCompleted;

        [DataMember(Name = "completed")] private bool _completed;
        [DataMember(Name = "hidden")] private bool _hidden;

        [DataMember(Name = "lastReportedDate")]
        private DateTime _lastReportedDate;

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
            private set { _completed = value; }
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


    [DataContract(Name = "AchievementDescriptions")]
    private class AchievementDescriptionsDto : IAchievementDescription
    {
        [DataMember(Name = "id")] private string _id;

        [DataMember(Name = "title")] private string _title;

        [DataMember(Name = "image")] private string _image;

        [DataMember(Name = "achievedDescription")]
        private string _achievedDescription;

        [DataMember(Name = "unachievedDescription")]
        private string _unachievedDescription;

        [DataMember(Name = "hidden")] private bool _hidden;

        [DataMember(Name = "points")] private int _points;

        public AchievementDescriptionsDto(IAchievementDescription achievement)
        {
            id = achievement.id;
            title = achievement.title;
            image = achievement.image;
            achievedDescription = achievement.achievedDescription;
            unachievedDescription = achievement.unachievedDescription;
            hidden = achievement.hidden;
            points = achievement.points;
        }

        public string id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string title
        {
            get { return _title; }
            private set { _title = value; }
        }

        public Texture2D image
        {
            get { return Util.Str2Texture(_image); }
            set { _image = Util.Texture2Str(value); }
        }

        public string achievedDescription
        {
            get { return _achievedDescription; }
            private set { _achievedDescription = value; }
        }

        public string unachievedDescription
        {
            get { return _unachievedDescription; }
            private set { _unachievedDescription = value; }
        }

        public bool hidden
        {
            get { return _hidden; }
            private set { _hidden = value; }
        }

        public int points
        {
            get { return _points; }
            private set { _points = value; }
        }
    }


    public void Sync(IAchievement[] achievements)
    {
        var toBeSaved = false;
        var temp = new AchievementDto[achievements.Length];
        if (Achievements == null)
        {
            for (var index = 0; index < achievements.Length; index++)
            {
                var achievement = achievements[index];
                temp[index] = new AchievementDto(achievement);
            }

            toBeSaved = true;
        }
        else
            for (var index = 0; index < achievements.Length; index++)
            {
                var platform = achievements[index];
                var local = Achievements.First(ach => ach.id == platform.id);

                // new is always better! :)
                if (local == null || local.lastReportedDate < platform.lastReportedDate)
                {
                    temp[index] = new AchievementDto(platform);
                    toBeSaved = true;
                }
                else
                {
                    temp[index] = local as AchievementDto;
                    Social.ReportProgress(platform.id, local.percentCompleted - platform.percentCompleted,
                                          success => Debug.Log(
                                              success
                                                  ? "Successfully reported achievement progress"
                                                  : "Failed to report achievement"));
                }
            }

        if (!toBeSaved) return;
        _gameData.Achievements = temp.ToList();
        Save();
    }

    public void Sync(ILocalUser localUser)
    {
        UserName = localUser.userName;
        ProfilePic = Util.Texture2Sprite(localUser.image);
        Save();
    }

    public void Sync(IAchievementDescription[] descriptions)
    {
        var temp = new AchievementDescriptionsDto[descriptions.Length];
        for (var index = 0; index < descriptions.Length; index++)
        {
            var achievement = descriptions[index];
            temp[index] = new AchievementDescriptionsDto(achievement);
        }

        _gameData.AchievementDescriptions = temp.ToList();
        Save();
    }

    public IAchievementDescription DescriptionOf(string buildingId)
    {
        return AchievementDescriptions.FirstOrDefault(description => description.id == buildingId);
    }
}