using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;

public class Util : MonoBehaviour
{
    private static Encoding _encoding = Encoding.UTF8;

    public static Sprite Str2Sprite(string pic)
    {
        if (string.IsNullOrEmpty(pic)) return null;
        var texByte = Convert.FromBase64String(pic);
        var tex = new Texture2D(128, 128);
        //load texture from byte array
        return tex.LoadImage(texByte) ? Sprite.Create(tex, new Rect(0, 0, 32, 32), new Vector2()) : null;
    }

    public static Sprite Texture2Sprite(Texture2D tex)
    {
        return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2());
    }


    public static Texture2D Sprite2Texture(Sprite value)
    {
        return value.texture;
    }

    public static string Sprite2Str(Sprite userProfilePic)
    {
        return userProfilePic == null ? "" : Convert.ToBase64String(userProfilePic.texture.EncodeToPNG());
    }

    public static string Texture2Str(Texture2D tex)
    {
        return tex == null ? "" : Convert.ToBase64String(tex.EncodeToPNG());
    }

    public static Texture2D Str2Texture(string pic)
    {
        if (string.IsNullOrEmpty(pic)) return null;
        var texByte = Convert.FromBase64String(pic);
        var tex = new Texture2D(32, 32);
        tex.LoadImage(texByte);
        //load texture from byte array
        return tex;
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

    public static readonly string SaveFilePath = Path.Combine(Application.persistentDataPath, "game.data");
    public static readonly string VideosFilePath = Path.Combine(Application.streamingAssetsPath, "videos");


    public static string Achievement2Str(Achievement achievement)
    {
        return JsonUtility.ToJson(achievement, true);
    }

    public static Achievement Str2Achievement(string achievement)
    {
        return JsonUtility.FromJson<Achievement>(achievement);
    }

    private static DriveWrapper _dw;

    public static string FindDriveId(string gameObjectName)
    {
        if (_dw == null)
        {
            var path_to = Path.Combine(Application.streamingAssetsPath, "video-files.json");
            using (var r = new StreamReader(path_to))
            {
                var json = r.ReadToEnd();
                _dw = JsonUtility.FromJson<DriveWrapper>(json);
            }
        }

        var index = 0;
        if (gameObjectName.EndsWith(")"))
            index = (int) char.GetNumericValue(gameObjectName[gameObjectName.Length - 2]);
        
        switch (SceneManagementUtil.ActiveScene)
        {
            case SceneManagementUtil.Scenes.Kabe: return _dw.fil[index];
            case SceneManagementUtil.Scenes.HzMuhammed: return _dw.hakem[index];
            case SceneManagementUtil.Scenes.Hamza: return _dw.hamza[index];
            case SceneManagementUtil.Scenes.Hatice: return _dw.kamer[index];
            case SceneManagementUtil.Scenes.Ebubekir: return _dw.hicret[index];
            // fall-through
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    [UsedImplicitly]
    private class DriveWrapper
    {
        public List<string> fil;
        public List<string> hakem;
        public List<string> hamza;
        public List<string> kamer;
        public List<string> hicret;
    }
}