using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class SiyerMainScreenManager : MonoBehaviour
{
    [SerializeField] private Camera _camera;


    [SerializeField] private List<string> _triggers;
    [SerializeField] private List<AudioClip> _audioClips;

    private Animator _animator;

    public GameObject Board;


    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Back();
        }

        if (Board.activeSelf) return;
        if (!Input.GetMouseButtonDown(0)) return;
        var pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        var hitInfo = Physics2D.Raycast(_camera.ScreenToWorldPoint(pos), Vector2.zero);
        if (hitInfo)
        {
            HandleTouchOn(hitInfo.transform.gameObject);
        }
    }

    [UsedImplicitly]
    public void Back()
    {
        SceneManagementUtil.Load(SceneManagementUtil.Scenes.Izometrik);
        if (!FindObjectOfType<MusicManager>()) return;
            FindObjectOfType<MusicManager>().GetComponent<AudioSource>().Stop();
    }

    private void HandleTouchOn(GameObject transformGameObject)
    {
        var index = _triggers.IndexOf(transformGameObject.name);
        if (index >= 0)
        {
            _animator.SetTrigger(transformGameObject.name);
        }
        else
            FindObjectOfType<BuildingManager>().Selection(transformGameObject);
    }

    // used by animations
    [UsedImplicitly]
    private void Sound(string triggerName)
    {
        var index = _triggers.IndexOf(triggerName);
        GetComponent<AudioSource>().PlayOneShot(_audioClips[index]);
    }


    private void MainCameraTransformUpdate()
    {
        _camera.transform.parent = null;
    }
}