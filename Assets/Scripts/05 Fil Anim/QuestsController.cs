using System.Collections;
using System.IO;
using System.Linq;
using System.Security.Policy;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

/*
 * This class manages all quests and switching between them.
 */
public class QuestsController : MonoBehaviour
{
    private static int _questIndex;

    public Quest[] Quests;

    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _previousButton;
    [SerializeField] private GameObject _questButton;
    [SerializeField] private GameObject _playPauseButton;
    public GameObject DownloadScreen;


    [SerializeField] private GameObject _autoPlay;

    [SerializeField] private GameObject _videoPanel;
    private VideoPlayer _videoPlayer;
    public CommonResources.Building Reward = CommonResources.Building.Abdulmuttalib;


    // Use this for initialization
    private void Start()
    {
        _questIndex = 0;
        StopBackgroundMusic();
        _videoPlayer = _videoPanel.GetComponent<VideoPlayer>();
        _videoPlayer.loopPointReached += EndVideo;
//        var unused = Quests.All(quest => quest.Completed = true);
        _autoPlay.GetComponent<VideoPlayer>().loopPointReached += player => NextQuest();
        // Check achievement Conditions
        if (Quests.All(quest => quest.Completed))
            UserManager.StorySuccess(Reward);
    }

    private static void StopBackgroundMusic()
    {
        if (FindObjectOfType<MusicManager>())
            FindObjectOfType<MusicManager>().GetComponent<AudioSource>().Stop();

        if (FindObjectOfType<DesertMusicManager>())
            FindObjectOfType<DesertMusicManager>().GetComponent<AudioSource>().Stop();
    }

    private void EndVideo(VideoPlayer source)
    {
        if (_videoPlayer.isPlaying)
            _videoPlayer.Pause();
//        Quests[_questIndex].Completed = true;
        EndQuest();
    }

    private void EndQuest()
    {
        Color color;
        if (!ColorUtility.TryParseHtmlString("#92FF00FF", out color)) return;
        if (_nextButton.interactable)
        {
            _nextButton.image.color = color;
            StartCoroutine(AutoPlay());
        }

        if (_previousButton.interactable)
        {
            _previousButton.image.color = color;
        }

        // Check achievement Conditions
        if (Quests.All(quest => quest.Completed))
            UserManager.StorySuccess(Reward);
    }

    private void UpdateButtonConditions()
    {
        // home button is normal
        // if have next button is active
        // if have back button is active
        _nextButton.image.color = Color.white;
        _previousButton.image.color = Color.white;

        _nextButton.interactable = _questIndex < Quests.Length - 1;
        _previousButton.interactable = _questIndex > 0;
        _playPauseButton.SetActive(Quests[_questIndex].VideoClipAvailable);
        _questButton.SetActive(Quests[_questIndex].HasQuestion && !Quests[_questIndex].Answered);
    }

    [UsedImplicitly]
    public void PlayPause()
    {
        if (_videoPlayer.isPlaying)
        {
            _videoPlayer.Pause();
            _playPauseButton.GetComponent<Animator>().SetTrigger("Pause");
        }
        else
        {
            _videoPlayer.Play();
            _playPauseButton.GetComponent<Animator>().SetTrigger("Play");
        }
    }

    [UsedImplicitly]
    public void Back()
    {
        if (FindObjectOfType<MusicManager>())
            FindObjectOfType<MusicManager>().GetComponent<AudioSource>().Play();
        if (FindObjectOfType<DesertMusicManager>())
            FindObjectOfType<DesertMusicManager>().GetComponent<AudioSource>().Play();
        SceneManagementUtil.Load(SceneManagementUtil.Scenes.AnaEkran);
    }

    [UsedImplicitly]
    public void NextQuest()
    {
        CloseQuestion();
        StopQuest();
        _questIndex++;
        InitiateQuest();
    }

    [UsedImplicitly]
    public void PreviousQuest()
    {
        CloseQuestion();
        StopQuest();
        _questIndex--;
        print(_questIndex);
        InitiateQuest();
    }

    private void StopQuest()
    {
        var quest = Quests[_questIndex];
        if (quest.VideoClipAvailable)
        {
            _videoPlayer.Stop();
        }

        _videoPanel.GetComponent<Animator>().Play("VideoIdle");
        _autoPlay.GetComponent<VideoPlayer>().Stop();
        _autoPlay.SetActive(false);
    }

    public void InitiateQuest()
    {
        if (VideosAvailable())
        {
            UpdateButtonConditions();
            var quest = Quests[_questIndex];
            StartCoroutine(PlayVideo(quest));
        }
        else
        {
            DownloadScreen.SetActive(true);
        }
    }

    private bool VideosAvailable()
    {
        return Quests.All(quest => quest.VideoClipAvailable);
    }

    [UsedImplicitly]
    public void Download()
    {
        Quests.All(quest => DownloadQuest(quest));
    }

    public bool DownloadQuest(Quest quest)
    {
        if (!quest.VideoClipAvailable)
        {
            StartCoroutine(DownloadFile(quest));
        }

        return true;
    }

    private IEnumerator DownloadFile(Quest quest)
    {
        using (var www = new WWW(quest.Url))
        {
            Debug.Log("Downloading...");
            yield return www;
            Debug.Log("Download finished..");
            File.WriteAllBytes(quest.VideoLocation, www.bytes);
        }
    }


    [UsedImplicitly]
    public void CloseQuestion()
    {
        var quest = Quests[_questIndex];
        if (quest.VideoClipAvailable && !_videoPlayer.isPlaying)
        {
            _videoPlayer.Play();
        }

        quest.gameObject.SetActive(false);
        quest.transform.parent.gameObject.SetActive(false);
    }

    [UsedImplicitly]
    public void OpenQuestion()
    {
        var quest = Quests[_questIndex];
        if (quest.VideoClipAvailable && _videoPlayer.isPlaying)
        {
            _videoPlayer.Pause();
        }

        quest.gameObject.SetActive(true);
        _questButton.SetActive(false);
    }

    private IEnumerator PlayVideo(Quest quest)
    {
        if (quest.isVideo)
            _videoPlayer.clip = quest.VideoClip;
        else
        {
            _videoPlayer.source = VideoSource.Url;
            Debug.Log(quest.VideoLocation);
            _videoPlayer.url = quest.VideoLocation;
        }

        _videoPlayer.Prepare();

        while (!_videoPlayer.isPrepared)
        {
            yield return null;
        }

        _videoPanel.GetComponent<RawImage>().texture = _videoPlayer.texture;
        _videoPanel.GetComponent<Animator>().SetTrigger("VideoStart");
        _videoPlayer.Play();
    }

    private IEnumerator AutoPlay()
    {
        _autoPlay.SetActive(true);
        var autoPlayer = _autoPlay.GetComponent<VideoPlayer>();
        autoPlayer.Prepare();

        while (!autoPlayer.isPrepared)
        {
            yield return null;
        }

        _autoPlay.GetComponent<RawImage>().texture = autoPlayer.texture;
        autoPlayer.Play();
    }
}