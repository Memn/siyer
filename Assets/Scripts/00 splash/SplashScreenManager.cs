using System.Collections;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using managers;
using UnityEngine;
using UnityEngine.Events;

public class SplashScreenManager : MonoBehaviour
{
    public AudioClip DesertMusic;
    public AudioClip BackgroundClip;
    public GameObject MergeRequestPanel;
    public UserSelector UserSelector;
    private bool _mergeRequestAnswer;
    private User _selectedUser;
    private bool _ready = true;
    private UserDb _userDb;

    private void Awake()
    {
        _userDb = new UserDb();
        
        if (!File.Exists(Util.QuestsFile))
        {
            LogUtil.Log("Quests File is missing trying to download...");
            StartCoroutine(Util.DownloadFile(Util.QuestsReference, www =>
            {
                // Save File
                File.WriteAllBytes(Util.QuestsFile, www.bytes);
            }));
        }
        else
        {
            LogUtil.Log("Checking quests file is sync or not!");
            Util.LoadQuestsFile();
            StartCoroutine(Util.DownloadFile(Util.QuestsReference, Util.SyncQuests));
        }

        if (FindObjectOfType<DesertMusicManager>())
            return;
        var component = new GameObject("DesertMusicManager").AddComponent<AudioSource>();
        component.gameObject.AddComponent<DesertMusicManager>();
        component.clip = DesertMusic;
        component.loop = true;
        component.volume = 0.4f;
        component.Play();
        DontDestroyOnLoad(component.gameObject);

        var musicManager = new GameObject("MusicManager").AddComponent<AudioSource>();
        musicManager.gameObject.AddComponent<MusicManager>();
        musicManager.clip = BackgroundClip;
        musicManager.loop = true;
        musicManager.volume = 0.1f;
        musicManager.playOnAwake = false;
        DontDestroyOnLoad(musicManager.gameObject);
    }

    public void Init()
    {
        StartCoroutine(WaitDBInit());
    }

    private IEnumerator WaitDBInit()
    {
        yield return new WaitUntil(() => _userDb != null);
        UserManagement.Instance.Init(callback => StartCoroutine(UserMergeSelection(callback)),
            (users, callback) => StartCoroutine(SelectUser(users, callback))
        );        
    }

    private IEnumerator SelectUser(List<User> users, UnityAction<User> callback)
    {
        _ready = false;
        UserSelector.Open(users, SelectionCompleted(users));
        yield return new WaitUntil(() => !UserSelector.IsOpen);
        _ready = true;
        callback(_selectedUser);
    }

    private UnityAction<string> SelectionCompleted(List<User> users)
    {
        return id =>
        {
            _selectedUser = users.Find(user => user.Id.Equals(id));
            UserSelector.Close();
        };
    }


    private IEnumerator UserMergeSelection(UnityAction<bool> callback)
    {
        _ready = false;
        MergeRequestPanel.SetActive(true);
        yield return new WaitUntil(() => !MergeRequestPanel.activeSelf);
        _ready = true;
        callback(_mergeRequestAnswer);
    }

    [UsedImplicitly]
    public void AnswerMergeRequest(bool answer)
    {
        _mergeRequestAnswer = answer;
        MergeRequestPanel.SetActive(false);
    }

    [UsedImplicitly]
    public IEnumerator LoadMainMenu()
    {
        yield return new WaitUntil(() => _ready);
        SceneManagementUtil.Load(SceneManagementUtil.Scenes.Izometrik);
    }
}