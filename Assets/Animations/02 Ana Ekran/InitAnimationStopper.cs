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

    [UsedImplicitly]
    private void Stop()
    {
        GetComponent<Animator>().cullingMode = AnimatorCullingMode.CullCompletely;
        FindObjectOfType<LevelManager>().ShowLevelQuestsInfo();
    }
}