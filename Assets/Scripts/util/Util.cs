using System;
using System.Collections.Generic;
using System.IO;
using Facebook.MiniJSON;
using UnityEngine;
using Random = UnityEngine.Random;

public class Util : MonoBehaviour
{
    public static string GetPictureURL(string facebookID, int? width = null, int? height = null, string type = null)
    {
        string url = string.Format("/{0}/picture", facebookID);
        string query = width != null ? "&width=" + width.ToString() : "";
        query += height != null ? "&height=" + height.ToString() : "";
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
        var texByte = Convert.FromBase64String(pic);
        var tex = new Texture2D(128, 128);
        //load texture from byte array
        return tex.LoadImage(texByte) ? Sprite.Create(tex, new Rect(0, 0, 128, 128), new Vector2()) : null;
    }

    public static string Sprite2Str(Sprite userProfilePic)
    {
        return userProfilePic == null ? "" : Convert.ToBase64String(userProfilePic.texture.EncodeToPNG());
    }
}