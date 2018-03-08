using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KutuphaneManager : MonoBehaviour
{
    public TextMesh topicHead;
    private Dictionary<string, string> _topicWords;

    [SerializeField] private Camera _camera;
    private KutuphaneMap _map;
    private int _topicIndex;

    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _puzzleScreen;

    private void Start()
    {
        _map = GetComponent<KutuphaneMap>();
        _topicWords = new Dictionary<string, string>
        {
            {"Sebze", "Hıyar Domates DolmaBiber Soğan Patlıcan"},
            {"Ülke", "Türkiye Almanya Fransa SuudiArabistan Lübnan Cezayir Finlandiya"},
            {"Meyve", "Üzüm Elma Armut Şeftali Erik"},
            {"Oyun", "GTA Siyer CallOfDuty"},
            {"Şehir", "İstanbul Ankara Moskova Berlin Tokyo Oslo Helsinki Kudüs"},
            {"Spor", "Tenis Futbol Top Beşiktaş Galatasaray Fenerbahçe"},
            {"Savaş", "Dandanakan Ridaniye Mercidabık Preveze"}
        };

        LoadNextTopic();
    }

    private void StartPuzzle()
    {
        string words;
        var topic = _topicWords.Keys.ElementAt(_topicIndex % _topicWords.Count);
        topicHead.text = topic;
        if (_topicWords.TryGetValue(topic, out words))
        {
            _map.StartPuzzle(words);
        }
    }

    private void Update()
    {
        // idle movements can be done here?
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManagementUtil.Load(SceneManagementUtil.Scenes.AnaEkran);
        }

        if (!Input.GetMouseButton(0)) return;

        var pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        var hitInfo = Physics2D.Raycast(_camera.ScreenToWorldPoint(pos), Vector2.zero);
        // RaycastHit2D can be either true or null, but has an implicit conversion to bool, so we can use it like this
        if (!hitInfo) return;
        if (hitInfo.transform.name == "Next")
        {
            LoadNextTopic();
        }

        // Here you can check hitInfo to see which collider has been hit, and act appropriately.
    }

    private void LoadNextTopic()
    {
        _puzzleScreen.SetActive(true);
        _winScreen.SetActive(false);
        _topicIndex++;
        StartPuzzle();
    }

    public void Congrats()
    {
        string[] messages = {"Aferin", "Tebrikler", "Bravo", "Helal", "Mükemmel", "Başarılı"};
        var r = Random.Range(0, messages.Length);
        _winScreen.transform.Find("Text").GetComponent<TextMesh>().text = messages[r];
        _winScreen.SetActive(true);
        _puzzleScreen.SetActive(false);
    }
}