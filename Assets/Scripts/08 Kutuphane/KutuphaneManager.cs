using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KutuphaneManager : MonoBehaviour
{
    public TextMesh topicHead;

    private static readonly Dictionary<string, string> TopicWords = new Dictionary<string, string>
    {
        {"Sebze", "Hıyar Domates DolmaBiber Soğan Patlıcan"},
        {"Ülke", "Türkiye Almanya SuudiArabistan Lübnan Cezayir Finlandiya"},
        {"Meyve", "Üzüm Elma Armut Şeftali Erik Hurma"},
        {"Gavur Şehirler", "Moskova Berlin Tokyo Oslo Helsinki"},
        {"Şehir", "Bakü Trabzon Bursa Aşkabat"},
        {"Sahabe 1", "OsmanbinAffan EbuUbeydebinCerrah"},
        {"Sahabe 2", "Abdurrahmanbinavf AlibinEbuTalib"},
        {"Sahabe 3", "Sa'dbinEbûVakkās SaidbinZeyd"},
        {"Mübarek Beldeler", "Mekke Medine Kudüs İstanbul Niğde"},
        {"Sünnet", "Okçuluk Yüzme"},
        {"Mübarek Hayvan", "Deve Koyun Keçi Kedi"},
        {"Savaş 1", "Dandanakan Ridaniye Mercidabık Preveze"},
        {"Savaş 2", "Bedir Uhud Hendek Hayber"},
        {"Savaş 3", "Şehit Gazi Gaza"},
    };

    [SerializeField] private Camera _camera;
    private KutuphaneMap _map;
    private int _topicIndex;

    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _puzzleScreen;

    private void Start()
    {
        _map = GetComponent<KutuphaneMap>();
        _topicIndex = Random.Range(0, TopicWords.Count);
        LoadNextTopic();
        StartPuzzle();
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
            StartPuzzle();
        }

        // Here you can check hitInfo to see which collider has been hit, and act appropriately.
    }

    private void LoadNextTopic()
    {
        _topicIndex = ++_topicIndex % TopicWords.Count;
        string words;
        if (TopicWords.TryGetValue(TopicWords.Keys.ElementAt(_topicIndex), out words))
        {
            _map.LoadPuzzle(words);
        }
    }

    private void StartPuzzle()
    {
        _puzzleScreen.SetActive(true);
        _winScreen.SetActive(false);
        topicHead.text = TopicWords.Keys.ElementAt(_topicIndex);
        _map.StartPuzzle();
    }

    public void Congrats()
    {
        string[] messages = {"Aferin", "Tebrikler", "Bravo", "Helal", "Mükemmel", "Başarılı"};
        var r = Random.Range(0, messages.Length);
        _winScreen.transform.Find("Text").GetComponent<TextMesh>().text = messages[r];
        _winScreen.SetActive(true);
        _puzzleScreen.SetActive(false);
        LoadNextTopic();
    }
}