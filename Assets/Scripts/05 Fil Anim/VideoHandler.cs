using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoHandler : MonoBehaviour
{
    [SerializeField] private GameObject _autoPlay;

    [SerializeField] private GameObject _videoPanel;

    [SerializeField] private GameObject _playPauseButton;

    private VideoPlayer _videoPlayer;
    private QuestsController _controller;

    public GameObject PlayPauseButton
    {
        get { return _playPauseButton; }
    }

    private void Start()
    {
        _controller = GetComponent<QuestsController>();
        _videoPlayer = _videoPanel.GetComponent<VideoPlayer>();
        _videoPlayer.loopPointReached += EndVideo;
        _autoPlay.GetComponent<VideoPlayer>().loopPointReached += player => _controller.NextQuest();
    }

    private void EndVideo(VideoPlayer source)
    {
        if (_videoPlayer.isPlaying)
            _videoPlayer.Pause();
        _controller.EndQuest();
    }

    internal IEnumerator PlayVideo(Quest quest)
    {
        _videoPlayer.source = VideoSource.Url;
        _videoPlayer.url = quest.VideoLocation;

        _videoPlayer.Prepare();

        while (!_videoPlayer.isPrepared)
        {
            yield return null;
        }

        _videoPanel.GetComponent<RawImage>().texture = _videoPlayer.texture;
        _videoPanel.GetComponent<Animator>().SetTrigger("VideoStart");
        _videoPlayer.Play();
    }

    internal IEnumerator AutoPlay()
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

    internal void Stop()
    {
        _videoPlayer.Stop();
        _videoPanel.GetComponent<Animator>().Play("VideoIdle");
        _autoPlay.GetComponent<VideoPlayer>().Stop();
        _autoPlay.SetActive(false);
    }

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

    internal void Pause()
    {
        if (_videoPlayer.isPlaying)
            PlayPause();
    }

    internal void Play()
    {
        if (!_videoPlayer.isPlaying)
            PlayPause();
    }
}