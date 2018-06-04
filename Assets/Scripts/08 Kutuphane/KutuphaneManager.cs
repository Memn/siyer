using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class KutuphaneManager : MonoBehaviour
{
    public Text topicHead;
    public Text Scoreboard;

    [SerializeField] private Camera _camera;
    private KutuphaneMap _map;

    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _puzzleScreen;

    private Dictionary<string, string>.Enumerator _enumerator;

    private Dictionary<string, string> _dict;

    private float _startTime;
    private float _spentTime = 0;

    private void Start()
    {
        _map = GetComponent<KutuphaneMap>();
        _dict = WordUtil.FindDictionaryByLevel(UserManager.Game.Level);
        _enumerator = _dict.GetEnumerator();
        UpdateScore(0);
        StartGame();
    }

    public void StartGame()
    {
        LoadNextTopic();
        StartPuzzle();
        _startTime = Time.time;
    }


    private void Update()
    {
        // idle movements can be done here?
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Back();
        }

        if (!Input.GetMouseButton(0)) return;

        var pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        var hitInfo = Physics2D.Raycast(_camera.ScreenToWorldPoint(pos), Vector2.zero);
        // RaycastHit2D can be either true or null, but has an implicit conversion to bool, so we can use it like this
        if (!hitInfo) return;
        if (hitInfo.transform.name == "Next") StartPuzzle();

        // Here you can check hitInfo to see which collider has been hit, and act appropriately.
    }

    [UsedImplicitly]
    public void Back()
    {
        SceneManagementUtil.Load(SceneManagementUtil.Scenes.AnaEkran);
    }

    private void LoadNextTopic()
    {
        _spentTime += Time.time - _startTime;
        _startTime = Time.time;

        if (!_enumerator.MoveNext())
        {
            UserManager.KelimelikSuccess(int.Parse(Scoreboard.text), _spentTime);
            EndOfLevel();
            return;
        }

        var current = _enumerator.Current;
        topicHead.text = string.Join("\n", current.Key.Split());
        _map.LoadPuzzle(current.Value);
    }


    private void StartPuzzle()
    {
        _puzzleScreen.SetActive(true);
        _winScreen.SetActive(false);
        _map.StartPuzzle();
    }

    public void Congrats()
    {
        string[] messages = {"Aferin", "Tebrikler", "Canavar", "Helal", "Mükemmel", "Başarılı"};
        var r = Random.Range(0, messages.Length);
        _winScreen.transform.Find("Text").GetComponent<TextMesh>().text = messages[r];
        _winScreen.SetActive(true);
        _puzzleScreen.SetActive(false);
        LoadNextTopic();
    }

    private void EndOfLevel()
    {
        _winScreen.transform.Find("Text").GetComponent<TextMesh>().text = "Seviye Tamamlandi";
        _winScreen.transform.Find("Next").gameObject.SetActive(false);
        _puzzleScreen.SetActive(false);
        Invoke("Back", 2);
    }

    public void UpdateScore(int score)
    {
        Scoreboard.text = score.ToString();
    }
}