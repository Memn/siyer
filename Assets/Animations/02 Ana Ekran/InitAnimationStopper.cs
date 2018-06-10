using JetBrains.Annotations;
using UnityEngine;

public class InitAnimationStopper : MonoBehaviour
{
    private static bool _init = true;

    private void Start()
    {
        if (!_init) return;
        GetComponent<Animator>().Play("Init");
        GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
        _init = false;
    }

    public AudioClip BackgroundClip;
    [UsedImplicitly]
    private void Stop()
    {
        GetComponent<Animator>().cullingMode = AnimatorCullingMode.CullCompletely;
        FindObjectOfType<LevelManager>().ShowLevelQuestsInfo();

        if (FindObjectOfType<MusicManager>()) return;
        var component = new GameObject("MusicManager").AddComponent<AudioSource>();
        component.gameObject.AddComponent<MusicManager>();
        component.clip = BackgroundClip;
        component.loop = true;
        component.volume = 0.1f;
        component.playOnAwake = false;
        DontDestroyOnLoad(component.gameObject);
        component.Play();
    }
}