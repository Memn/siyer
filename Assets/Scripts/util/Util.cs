using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Facebook.MiniJSON;
using GooglePlayGames;
using GooglePlayGames.BasicApi.SavedGame;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;
using Random = UnityEngine.Random;

public class Util : MonoBehaviour
{
    private static Encoding _encoding = Encoding.UTF8;

    public static string GetPictureURL(string facebookID, int? width = null, int? height = null, string type = null)
    {
        string url = string.Format("/{0}/picture", facebookID);
        string query = width != null ? "&width=" + width : "";
        query += height != null ? "&height=" + height : "";
        query += type != null ? "&type=" + type : "";
        if (query != "") url += ("?g" + query);
        return url;
    }

    public static Dictionary<string, string> RandomFriend(List<object> friends)
    {
        var fd = ((Dictionary<string, object>) (friends[Random.Range(0, friends.Count - 1)]));
        var friend = new Dictionary<string, string>();
        friend["id"] = (string) fd["id"];
        friend["first_name"] = (string) fd["first_name"];
        return friend;
    }

    public static Dictionary<string, string> DeserializeJSONProfile(string response)
    {
        var responseObject = Json.Deserialize(response) as Dictionary<string, object>;
        object nameH;
        var profile = new Dictionary<string, string>();
        if (responseObject.TryGetValue("first_name", out nameH))
        {
            profile["first_name"] = (string) nameH;
        }

        return profile;
    }

    public static List<object> DeserializeScores(string response)
    {
        var responseObject = Json.Deserialize(response) as Dictionary<string, object>;
        object scoresh;
        var scores = new List<object>();
        if (responseObject.TryGetValue("data", out scoresh))
        {
            scores = (List<object>) scoresh;
        }

        return scores;
    }

    public static List<object> DeserializeJSONFriends(string response)
    {
        var responseObject = Json.Deserialize(response) as Dictionary<string, object>;
        object friendsH;
        var friends = new List<object>();
        if (responseObject.TryGetValue("friends", out friendsH))
        {
            friends = (List<object>) (((Dictionary<string, object>) friendsH)["data"]);
        }

        return friends;
    }


    public static void DrawActualSizeTexture(Vector2 pos, Texture texture, float scale = 1.0f)
    {
        Rect rect = new Rect(pos.x, pos.y, texture.width * scale, texture.height * scale);
        GUI.DrawTexture(rect, texture);
    }

    public static void DrawSimpleText(Vector2 pos, GUIStyle style, string text)
    {
        Rect rect = new Rect(pos.x, pos.y, Screen.width, Screen.height);
        GUI.Label(rect, text, style);
    }

    public static User LoadUserFromFile(string userFilePath)
    {
        // Path.Combine combines strings into a file path
        Debug.Log(string.Format("looking for path {0} to load user!", userFilePath));
        User user;
        if (File.Exists(userFilePath))
        {
            // Read the json from the file into a string
            var dataAsJson = File.ReadAllText(userFilePath);
            // Pass the json to JsonUtility, and tell it to create a GameData object from it
            user = JsonUtility.FromJson<RawUser>(dataAsJson).ToUser();
            Debug.Log("User Loaded.");
        }
        else
        {
            Debug.LogError("user file does not exists!");
            Debug.Log("Saving default user");
            user = User.Default;
        }

        return user;
    }

    public static void SaveUser(User user, string userFilePath)
    {
        // Convert the instance ('this') of this class to a JSON string with "pretty print" (nice indenting).
        var json = JsonUtility.ToJson(new RawUser(user), true);
        // Write that JSON string to the specified file.
        File.WriteAllText(userFilePath, json);
        Debug.Log("User saved!");
    }

    public static Sprite Str2Sprite(string pic)
    {
        if (string.IsNullOrEmpty(pic)) return null;
        var texByte = Convert.FromBase64String(pic);
        var tex = new Texture2D(128, 128);
        //load texture from byte array
        return tex.LoadImage(texByte) ? Sprite.Create(tex, new Rect(0, 0, 128, 128), new Vector2()) : null;
    }

    public static Sprite Texture2Sprite(Texture2D tex)
    {
        return Sprite.Create(tex, new Rect(0, 0, 128, 128), new Vector2());
    }

    public static string Sprite2Str(Sprite userProfilePic)
    {
        return userProfilePic == null ? "" : Convert.ToBase64String(userProfilePic.texture.EncodeToPNG());
    }


    public static void Load<T>(GameObject parent, GameObject prefab, IEnumerable<T> leaderboard,
        UnityAction<GameObject, T> action)
    {
        foreach (var member in leaderboard)
        {
            var memberObj = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            action(memberObj, member);
            memberObj.transform.SetParent(parent.transform);
            memberObj.transform.localScale = Vector3.one;
        }
    }

    public static void ClearChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }
    }


    // saving / loading
    private static ISavedGameClient SavedGame
    {
        get { return ((PlayGamesPlatform) Social.Active).SavedGame; }
    }

    private static readonly string UserFilePath = Path.Combine(Application.persistentDataPath, "Siyer.data");

    public static void LocalSave(Game game)
    {
        // Convert the instance ('this') of this class to a JSON string with "pretty print" (nice indenting).
        // Write that JSON string to the specified file.
        File.WriteAllText(UserFilePath, GameSaveString(game));
        Debug.Log("Game saved!");
    }

    public static Game LocalLoad()
    {
        Debug.Log(string.Format("looking for path {0} to load game!", UserFilePath));
        Game game;
        if (File.Exists(UserFilePath))
        {
            // Read the json from the file into a string
            var dataAsJson = File.ReadAllText(UserFilePath);
            // Pass the json to JsonUtility, and tell it to create a GameData object from it
            game = JsonUtility.FromJson<RawGame>(dataAsJson).ToGame();
            Debug.Log("Game Loaded.");
        }
        else
        {
            Debug.LogError("game file does not exists!");
            game = Game.Default;
            LocalSave(game);
        }

        return game;
    }

    private static string GameSaveString(Game game)
    {
        // Convert the instance ('this') of this class to a JSON string with "pretty print" (nice indenting).
        return JsonUtility.ToJson(new RawGame(game), true);
    }

    public static void CloudSave(ISavedGameMetadata metadata, Game game)
    {
        var data = _encoding.GetBytes(GameSaveString(game));
        var upd = new SavedGameMetadataUpdate.Builder().Build();
        // writing
        SavedGame.CommitUpdate(metadata, upd, data, (s, m) => { });
    }

    public static Game CloudLoad(ISavedGameMetadata metadata)
    {
        var game = Game.Default;
        // reading
        SavedGame.ReadBinaryData(metadata, (status, data) =>
        {
            if (status != SavedGameRequestStatus.Success) return;
            game = JsonUtility.FromJson<RawGame>(_encoding.GetString(data)).ToGame();
        });
        return game;
    }

    public static string Achievement2Str(Achievement achievement)
    {
        return JsonUtility.ToJson(achievement, true);
    }

    public static Achievement Str2Achievement(string achievement)
    {
        return JsonUtility.FromJson<Achievement>(achievement);
    }
}