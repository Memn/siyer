using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

public class Util : MonoBehaviour
{
    public static Sprite Texture2Sprite(Texture2D tex)
    {
        return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2());
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
            Destroy(child.gameObject);
    }

    public static readonly string SaveFilePath = Path.Combine(Application.persistentDataPath, "game.data");

    public static IEnumerator DownloadFile(string id, string saveTo)
    {
        using (var www = new WWW(UrlForGoogleId(id)))
        {
            yield return www;
            File.WriteAllBytes(saveTo, www.bytes);
        }
    }

    public const string QuestsReference = "1eELGWpIkqh4fB_w9k519gLumn9p4-KCM";
    public static readonly string QuestsFile = Path.Combine(Application.persistentDataPath, "quests.json");

    private static QuestsWrapper _qw;

    public static QuestDto[] InitQuests()
    {
        if (_qw == null)
        {
            // wait for quests file to download.
            while (!File.Exists(QuestsFile))
            {
            }

            var result = File.ReadAllText(QuestsFile);
            _qw = JsonUtility.FromJson<QuestsWrapper>(result);
        }


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

    [UsedImplicitly]
    private class QuestsWrapper
    {
        public QuestDto[] fil;
        public QuestDto[] hakem;
        public QuestDto[] hamza;
        public QuestDto[] kamer;
        public QuestDto[] hicret;
    }

    [Serializable]
    public class QuestDto
    {
        public Question Question;
        public string Url;
        public bool Completed;
    }


    public static void SaveQuest(Quest quest, int questIndex)
    {
        QuestDto questDto;
        // ReSharper disable once SwitchStatementMissingSomeCases
        switch (SceneManagementUtil.ActiveScene)
        {
            case SceneManagementUtil.Scenes.Kabe:
                questDto = _qw.fil[questIndex];
                break;
            case SceneManagementUtil.Scenes.HzMuhammed:
                questDto = _qw.hakem[questIndex];
                break;
            case SceneManagementUtil.Scenes.Hamza:
                questDto = _qw.hamza[questIndex];
                break;
            case SceneManagementUtil.Scenes.Hatice:
                questDto = _qw.kamer[questIndex];
                break;
            case SceneManagementUtil.Scenes.Ebubekir:
                questDto = _qw.hicret[questIndex];
                break;
            // fall-through
            default:
                throw new ArgumentOutOfRangeException();
        }

        questDto.Question = quest.Question;
        questDto.Completed = quest.Completed;

        File.WriteAllText(QuestsFile, JsonUtility.ToJson(_qw, true));
    }

    public static string QuestName()
    {
        // ReSharper disable once SwitchStatementMissingSomeCases
        switch (SceneManagementUtil.ActiveScene)
        {
            case SceneManagementUtil.Scenes.Kabe: return "fil";
            case SceneManagementUtil.Scenes.HzMuhammed: return "hakem";
            case SceneManagementUtil.Scenes.Hamza: return "hamza";
            case SceneManagementUtil.Scenes.Hatice: return "kamer";
            case SceneManagementUtil.Scenes.Ebubekir: return "hicret";
            // fall-through
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}