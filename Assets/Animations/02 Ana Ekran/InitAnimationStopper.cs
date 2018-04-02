using UnityEngine;

public class InitAnimationStopper : MonoBehaviour
{
    private static bool _init = true;

    private void Start()
    {
        if (!_init) return;
        GetComponent<Animator>().Play("Init");
        _init = false;
    }

    private void Stop()
    {
        GetComponent<Animator>().cullingMode = AnimatorCullingMode.CullCompletely;
    }
}