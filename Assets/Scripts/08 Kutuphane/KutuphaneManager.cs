using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class KutuphaneManager : MonoBehaviour
{
    public Text topicHead;


    private static readonly Dictionary<string, string> TopicWords = new Dictionary<string, string>
    {
        {"Kıblemiz", "Kâbe"},
        {"İlahi Kitap", "Kuran"},
        {"Kuran'ın İndirildiği Şehirler", "Mekke Medine"},
        {"Bilal-i Habeşi", "Ezan Müezzin"},
        {"Peygamber", "Nebi Resul"},
        {"Hz. Ömer", "Faruk Halife Adalet"},
        {"Hira", "Nur Oku Vahiy Cebrail"},
        {"Mirac", "İsra Kudüs Namaz"},
        {"İbadet", "Namaz Farz Sünnet Nafile Vacib"},
        {"Hicret", "Habeşistan Sevr Medine Ensar Muhacir"},
        {"İslam", "KelimeiŞahadet Namaz Oruç Zekat Hac"},
        {"Ahiret", "Ölüm Azrail Cennet Cehennem Kabir"},
        {"Cahiliye", "Darunnedve Putlar Zulüm İşkence Müşrik"},
    };

    [SerializeField] private Camera _camera;
    private KutuphaneMap _map;
    private int _topicIndex;

    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _puzzleScreen;

    public UserManager UserManager;

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
        var topicWords = TopicWords.Keys.ElementAt(_topicIndex).Split();
        topicHead.text = string.Join("\n", topicWords);
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
}