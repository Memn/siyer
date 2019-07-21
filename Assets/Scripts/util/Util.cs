using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
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

    public static void LoadSingle<T>(GameObject parent, GameObject prefab, T member, UnityAction<GameObject, T> action)
    {
        var memberObj = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        action(memberObj, member);
        memberObj.transform.SetParent(parent.transform);
        memberObj.transform.localScale = Vector3.one;
    }

    public static void ClearChildren(Transform parent)
    {
        foreach (Transform child in parent)
            Destroy(child.gameObject);
    }

    public static IEnumerator DownloadFile(string id, UnityAction<WWW> callback, UnityAction<WWW> progress = null,
        UnityAction<WWW> error = null)
    {
        LogUtil.Log("Downloading file from Google drive with id:" + id);
        using (var www = new WWW(UrlForGoogleId(id)))
        {
            if (progress != null)
                progress(www);
            yield return www;
            if (callback != null && www.bytes.Length != 0)
                callback(www);
            if (error != null && www.bytes.Length == 0)
                error(www);
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

    public static string UrlForGoogleId(string id)
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

        public bool Sync(QuestsWrapper qw)
        {
            var sync = Util.Sync(fil, qw.fil, "fil");
            sync = sync && Util.Sync(hakem, qw.hakem, "hakem");
            sync = sync && Util.Sync(hamza, qw.hamza, "hamza");
            sync = sync && Util.Sync(kamer, qw.kamer, "kamer");
            sync = sync && Util.Sync(hicret, qw.hicret, "hicret");
            return sync;
        }
    }

    public static bool Sync(QuestDto[] chapter, QuestDto[] candidates, string chapterName)
    {
        var sync = true;
        for (var i = 0; i < candidates.Length; i++)
        {
            if (chapter[i].Sync(candidates[i], chapterName, i)) continue;
            LogUtil.Log(string.Format("chapter: {0} episode: {1} is not sync!!", chapterName, i));
            sync = false;
        }

        return sync;
    }

    [Serializable]
    public class QuestDto
    {
        public Question Question;
        public string Url;
        public bool Completed;

        public bool Sync(QuestDto quest, string chapter, int index)
        {
            if (Url == quest.Url)
                return Question.Sync(quest.Question);
            LogUtil.Log("Video URL is changed. Clearing downladed video from cache");
            //filQuest(2).mp4
            ClearCache(string.Format("{0}Quest({1}).mp4", chapter, index + 1));
            Url = quest.Url;
            Question.Sync(quest.Question);
            return false;
        }
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

        questDto.Question.Answered = quest.Question.Answered;
        questDto.Completed = quest.Completed;

        SaveQuestsFile();
    }

    private static void SaveQuestsFile()
    {
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

    public static void LoadQuestsFile()
    {
        if (_qw != null) return;
        // wait for quests file to download.
        var result = File.ReadAllText(QuestsFile);
        _qw = JsonUtility.FromJson<QuestsWrapper>(result);
    }

    public static void SyncQuests(WWW www)
    {
        var json = Encoding.UTF8.GetString(www.bytes).Trim();
        var qw = JsonUtility.FromJson<QuestsWrapper>(json);
        if (_qw.Sync(qw))
        {
            LogUtil.Log("Quests are synced already.");
            return;
        }

        LogUtil.Log("Quests are synced. Saving! ");
        SaveQuestsFile();
        LogUtil.Log("Quests are saved. ");
    }

    public static void ClearCache(string fileName)
    {
        var saveFilePath = Path.Combine(Application.persistentDataPath, fileName);
        LogUtil.Log("Trying to clear: " + saveFilePath);
        File.Delete(saveFilePath);
        LogUtil.Log("Clean successful.");
    }
}