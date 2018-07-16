using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class FilAnimController : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private Animator _animator;
    [SerializeField] private Animator _videoPanelAnimator;

    [SerializeField] private GameObject _quest;
    [SerializeField] private GameObject _skipButton;
    [SerializeField] private GameObject _autoPlay;
    [SerializeField] private RawImage _rawImage;

    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _previousButton;


    private static int _videoIndex = 0;

    public VideoClip[] VideoClips;


    private void Start()
    {
        _videoPlayer.loopPointReached += EndReached;
        StartCoroutine(PlayVideo());
    }

    private IEnumerator PlayVideo()
    {
        _videoPlayer.clip = VideoClips[_videoIndex];
        UpdateButtonConditions();
        _videoPlayer.Prepare();
        while (!_videoPlayer.isPrepared)
        {
            yield return null;
        }

        _rawImage.texture = _videoPlayer.texture;
        _videoPanelAnimator.SetTrigger("VideoStart");
        _videoPlayer.Play();
    }

    private void UpdateButtonConditions()
    {
        // home button is normal
        // if have next button is active
        // if have back button is active
        _nextButton.image.color = Color.white;
        _previousButton.image.color = Color.white;
       
        _nextButton.interactable = _videoIndex < VideoClips.Length - 1;
        _previousButton.interactable = _videoIndex > 0;
    }

    private void EndReached(VideoPlayer source)
    {
        // some end operations
        if (_videoPlayer.isPlaying)
        {
            _videoPlayer.Pause();
        }

        Color color;
        if (!ColorUtility.TryParseHtmlString("#92FF00FF", out color)) return;
        if (_nextButton.interactable)
        {
            _nextButton.image.color = color;
            _autoPlay.SetActive(true);
            var auto = _autoPlay.GetComponent<VideoPlayer>();
            auto.loopPointReached += player => NextVideo();
            auto.Play();
        }

        if (_previousButton.interactable)
        {
            _previousButton.image.color = color;
        }
    }

    public void NextVideo()
    {
        _videoPlayer.Stop();
        _videoIndex++;
        StartCoroutine(PlayVideo());
    }

    public void PreviousVideo()
    {
        _videoPlayer.Stop();
        _videoIndex--;
        StartCoroutine(PlayVideo());
    }

    private void StartQuest()
    {
        _skipButton.SetActive(false);
        _quest.SetActive(true);
    }

    [UsedImplicitly]
    public void PlayPause()
    {
        if (_videoPlayer.isPlaying)
        {
            _videoPlayer.Pause();
            _animator.SetTrigger("Pause");
        }
        else
        {
            _videoPlayer.Play();
            _animator.SetTrigger("Play");
        }
    }

    [UsedImplicitly]
    public void Skip()
    {
        if (_videoPlayer.isPlaying)
        {
            _videoPlayer.Pause();
        }

        StartQuest();
    }

    [UsedImplicitly]
    public void Back()
    {
        SceneManagementUtil.Load(SceneManagementUtil.Scenes.AnaEkran);
    }
}