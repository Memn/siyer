using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;
using UnityEngine;

public class GameSaveLoadHelper
{
    public static void Save(Game game, string fileName = "game.data")
    {
        var saveFilePath = Path.Combine(Application.persistentDataPath, fileName);
        LogUtil.Log("Trying to save to: " + saveFilePath);
        var ds = new DataContractSerializer(typeof(GameData));
        var file = File.Open(saveFilePath, FileMode.OpenOrCreate);
        var writer = XmlDictionaryWriter.CreateTextWriter(file);
        ds.WriteObject(writer, game._gameData);
        writer.Close();
        file.Close();
        LogUtil.Log("Saved successfully.");
    }

    public static Game Load(string fileName = "game.data")
    {
        var loadFilePath = Path.Combine(Application.persistentDataPath, fileName);
        LogUtil.Log("Checking " + loadFilePath + " for saves.");
        if (!File.Exists(loadFilePath))
            return new Game {_gameData = new GameData {Achievements = InitAllAchievements()}};

        var file = File.Open(loadFilePath, FileMode.Open);
        var reader = XmlDictionaryReader.CreateTextReader(file, new XmlDictionaryReaderQuotas());

        var ds = new DataContractSerializer(typeof(GameData));
        LogUtil.Log("Loading game from " + loadFilePath);
        var gameData = ds.ReadObject(reader) as GameData;
        reader.Close();
        file.Close();
        LogUtil.Log(gameData == null ? "Game loading failed" : "Game Loaded Successfully");
        return gameData == null
            ? new Game {_gameData = new GameData {Achievements = InitAllAchievements()}}
            : new Game {_gameData = gameData};
    }

    private static List<AchievementDto> InitAllAchievements()
    {
        return CommonResources.AllAchievements().Select(id =>
        {
            var achievement = Social.CreateAchievement();
            achievement.id = id;
            achievement.percentCompleted = 0;
            return new AchievementDto(achievement);
        }).ToList();
    }

    public static void ClearCache(string fileName = "game.data")
    {
        var saveFilePath = Path.Combine(Application.persistentDataPath, fileName);
        LogUtil.Log("Trying to clear: " + saveFilePath);
        File.Delete(saveFilePath);
        LogUtil.Log("Clean successful.");
    }
}