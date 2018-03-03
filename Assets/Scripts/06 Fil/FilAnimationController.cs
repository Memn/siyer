using UnityEngine;

public class FilAnimationController : MonoBehaviour
{
    private int scene = 1;
    private AudioSource source;
    public AudioClip[] ebreheClips;

    public AudioClip[] dedeClips;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // idle movements can be done here?
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManagementUtil.Load(SceneManagementUtil.Scenes.AnaEkran);
        }
    }

    void EbrehePlay(int index)
    {
        source.PlayOneShot(ebreheClips[index]);
    }
}