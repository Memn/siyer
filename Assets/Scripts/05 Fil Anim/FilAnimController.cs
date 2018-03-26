using System.Collections;
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

    [SerializeField] private RawImage _rawImage;

    private void Start()
    {
        _videoPlayer.loopPointReached += EndReached;
        StartCoroutine(PlayVideo());
    }

    private IEnumerator PlayVideo()
    {
        _videoPlayer.Prepare();
        while (!_videoPlayer.isPrepared)
        {
            yield return null;
        }
        
        _rawImage.texture = _videoPlayer.texture;
        _videoPanelAnimator.SetTrigger("VideoStart");
        _videoPlayer.Play();
    }

    private void EndReached(VideoPlayer source)
    {
        Invoke("StartQuest", 1.0f);
    }

    private void StartQuest()
    {
        _skipButton.SetActive(false);
        _quest.SetActive(true);
    }

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

    public void Skip()
    {
        if (_videoPlayer.isPlaying)
        {
            _videoPlayer.Stop();
        }

        StartQuest();
    }

    public void Back()
    {
        SceneManagementUtil.Load(SceneManagementUtil.Scenes.AnaEkran);
    }
}