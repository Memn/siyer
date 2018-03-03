using System.Collections.Generic;
using UnityEngine;

public class SiyerMainScreenManager : MonoBehaviour
{
    [SerializeField] private Camera _camera;


    [SerializeField] private List<string> _triggers;
    [SerializeField] private List<AudioClip> _audioClips;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManagementUtil.Load(SceneManagementUtil.Scenes.MainMenu);
        }

        if (!Input.GetMouseButtonDown(0)) return;
        var pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        var hitInfo = Physics2D.Raycast(_camera.ScreenToWorldPoint(pos), Vector2.zero);
        // RaycastHit2D can be either true or null, but has an implicit conversion to bool, so we can use it like this
        if (hitInfo)
        {
            HandleTouchOn(hitInfo.transform.gameObject);
            // Here you can check hitInfo to see which collider has been hit, and act appropriately.
        }
    }

    private void HandleTouchOn(GameObject transformGameObject)
    {
        var index = _triggers.IndexOf(transformGameObject.name);
        if (index >= 0)
        {
            GetComponent<AudioSource>().PlayOneShot(_audioClips[index]);
            _animator.SetTrigger(transformGameObject.name);
        }
        else if (transformGameObject.name == "Talimhane-Bina")
        {
            SceneManagementUtil.Load(SceneManagementUtil.Scenes.Talimhane);
        }
        else if (transformGameObject.name == "Labirent-Bina")
        {
            SceneManagementUtil.Load(SceneManagementUtil.Scenes.Labirent);
        }
        else if (transformGameObject.name == "SoruCevap-Bina")
        {
            SceneManagementUtil.Load(SceneManagementUtil.Scenes.SoruCevap);
        }
        else if (transformGameObject.name == "Kabe-Bina")
        {
            SceneManagementUtil.Load(SceneManagementUtil.Scenes.FilVakasi);
        }
    }
}