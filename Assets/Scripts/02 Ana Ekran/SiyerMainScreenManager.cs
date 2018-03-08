using System.Collections.Generic;
using JetBrains.Annotations;
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
        //Invoke("InitAnimation", 1.0f);
    }
//
//    private void InitAnimation()
//    {
//        _animator.SetTrigger("Bulutlar");
//    }

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
            _animator.SetTrigger(transformGameObject.name);
        }
        else
            switch (transformGameObject.name)
            {
                case "Mountain":
                    _animator.SetTrigger("Bulutlar");
                    break;
                default:
                    GetComponent<BuildingManager>().Select(transformGameObject);
                    break;
//                case "Talimhane-Bina":
//                    SceneManagementUtil.Load(SceneManagementUtil.Scenes.Talimhane);
//                    break;
//                case "Labirent-Bina":
//                    SceneManagementUtil.Load(SceneManagementUtil.Scenes.Labirent);
//                    break;
//                case "SoruCevap-Bina":
//                    SceneManagementUtil.Load(SceneManagementUtil.Scenes.SoruCevap);
//                    break;
//                case "Kabe-Bina":
//                    SceneManagementUtil.Load(SceneManagementUtil.Scenes.FilVakasi);
//                    break;
            }
    }

    // used by animations
    [UsedImplicitly]
    private void Sound(string triggerName)
    {
        var index = _triggers.IndexOf(triggerName);
        GetComponent<AudioSource>().PlayOneShot(_audioClips[index]);
    }
}