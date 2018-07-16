using JetBrains.Annotations;
using UnityEngine;

public class SplashScreenManager : MonoBehaviour
{
    public AudioClip DesertMusic;
    public AudioClip BackgroundClip;

    private void Awake()
    {
        UserManager.Instance.Init();
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

    [UsedImplicitly]
    public void LoadMainMenu()
    {
        SceneManagementUtil.Load(SceneManagementUtil.Scenes.Izometrik);
    }
}