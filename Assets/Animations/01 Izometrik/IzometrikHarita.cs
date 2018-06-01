using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class IzometrikHarita : MonoBehaviour
{
    [SerializeField] private TextMesh _textMesh;
    [SerializeField] private Camera _camera;
  
    private enum Places
    {
        Medine,
        Kudus
    }

    private Dictionary<Places, string> _textDict;
    public GameObject InfoPanel;

    private void Start()
    {
        _textDict = new Dictionary<Places, string>
        {
            {Places.Medine, "Medineye varamadım gül kokusu alamadım."},
            {Places.Kudus, "Kudus İslam Uygarlığı için önemli bir merkezdir.\n Miraç hadisesi burada meydana gelmiştir"}
        };
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManagementUtil.Quit();
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
        if (InfoPanel.activeSelf) return;
        switch (transformGameObject.name)
        {
            case "kudus":
                GetComponent<Animator>().SetTrigger("kudus");
                break;
            case "medine":
                GetComponent<Animator>().SetTrigger("medine");
                break;
            default:
                EnterGame();
                break;
        }
    }

    public void EnterGame()
    {
        SceneManagementUtil.Load(SceneManagementUtil.Scenes.AnaEkran);
    }

    [UsedImplicitly]
    private void UpdateBoard(Places place)
    {
        string text;
        if (_textDict.TryGetValue(place, out text))
        {
            _textMesh.text = text;
        }
    }
}