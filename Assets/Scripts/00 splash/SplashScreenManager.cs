using JetBrains.Annotations;
using UnityEngine;

public class SplashScreenManager : MonoBehaviour
{
    public float LoadMainMenuAfter = 3;
    public AudioClip DesertMusic;

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
    }

    [UsedImplicitly]
    public void LoadMainMenu()
    {
        SceneManagementUtil.Load(SceneManagementUtil.Scenes.Izometrik);
    }
}