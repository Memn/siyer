using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
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

        // ReSharper disable once SwitchStatementMissingSomeCases
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

    public static IEnumerator DownloadFile(string id, string saveTo)
    {
//        Debug.Log(string.Format("Downloading from {0} to {1}", id, saveTo));
        using (var www = new WWW(UrlForGoogleId(id)))
        {
            yield return www;
            File.WriteAllBytes(saveTo, www.bytes);
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


    private static readonly string QuestsFile = Path.Combine(Application.streamingAssetsPath, "quests.json");

    private static QuestsWrapper _qw;

    public static Quest2[] InitQuests()
    {
        if (_qw == null)
            using (var reader = new StreamReader(Path.Combine(Application.streamingAssetsPath, QuestsFile)))
                _qw = JsonUtility.FromJson<QuestsWrapper>(reader.ReadToEnd());

        // ReSharper disable once SwitchStatementMissingSomeCases
        switch (SceneManagementUtil.ActiveScene)
        {
            case SceneManagementUtil.Scenes.Kabe: return _qw.fil;
            case SceneManagementUtil.Scenes.HzMuhammed: return _qw.hakem;
            case SceneManagementUtil.Scenes.Hamza: return _qw.hamza;
            case SceneManagementUtil.Scenes.Hatice: return _qw.kamer;
            case SceneManagementUtil.Scenes.Ebubekir: return _qw.hicret;
            // fall-through
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private static string UrlForGoogleId(string id)
    {
        return "https://docs.google.com/uc?export=download&id=" + id;
    }

    private class QuestsWrapper
    {
        public Quest2[] fil;
        public Quest2[] hakem;
        public Quest2[] hamza;
        public Quest2[] kamer;
        public Quest2[] hicret;
    }

    [Serializable]
    public class Quest2
    {
        public Question Question;
        public string Url;
        public bool Completed;
    }


    public static void SaveQuest(SceneManagementUtil.Scenes activeScene, global::Quest2 quest2, int questIndex)
    {
        Quest2 quest;
        // ReSharper disable once SwitchStatementMissingSomeCases
        switch (SceneManagementUtil.ActiveScene)
        {
            case SceneManagementUtil.Scenes.Kabe:
                quest = _qw.fil[questIndex];
                break;
            case SceneManagementUtil.Scenes.HzMuhammed:
                quest = _qw.hakem[questIndex];
                break;
            case SceneManagementUtil.Scenes.Hamza:
                quest = _qw.hamza[questIndex];
                break;
            case SceneManagementUtil.Scenes.Hatice:
                quest = _qw.kamer[questIndex];
                break;
            case SceneManagementUtil.Scenes.Ebubekir:
                quest = _qw.hicret[questIndex];
                break;
            // fall-through
            default:
                throw new ArgumentOutOfRangeException();
        }

        quest.Question = quest2.Question;
        quest.Completed = quest2.Completed;

        File.WriteAllText(QuestsFile, JsonUtility.ToJson(_qw, true));
    }
}