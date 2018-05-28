using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class KutuphaneManager : MonoBehaviour
{
    public Text topicHead;
    public Text Scoreboard;

    [SerializeField] private Camera _camera;
    private KutuphaneMap _map;
    private int _topicIndex;

    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _puzzleScreen;


    // filtered for level & grouped for topics
    private Dictionary<string, string> _lookup = new Dictionary<string, string>
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

    private Dictionary<string, string> _dict;
    private ILookup<string, Word> _lookup2;
    private IEnumerator<IGrouping<string, Word>> _lookupEnumerator;


    private void Awake()
    {
        var words = JsonUtility.FromJson<Wrapper>(WordUtil.Dict).Words.FindAll(kelime => kelime.Level == 1);
        _lookup2 = words.ToLookup(word => word.Topic, word => word);
        _lookupEnumerator = _lookup2.GetEnumerator();
        _dict = _lookup2.ToDictionary(grouping => grouping.Key, grouping =>
        {
            var combined = "";
            using (var wordEnumerator = grouping.GetEnumerator())
            {
                while (wordEnumerator.MoveNext())
                {
                    var w = wordEnumerator.Current;
                    if (w != null) combined += w.Text + " ";
                }
            }

            return combined;
        });

        Debug.Log(_lookup2.Count);
    }

    [Serializable]
    private class Wrapper
    {
        public List<Word> Words;
    }

    [Serializable]
    public class Word
    {
        public string Text;
        public string Topic;
        public int Level;
    }

    private void Start()
    {
        _map = GetComponent<KutuphaneMap>();

        _topicIndex = Random.Range(0, _lookup.Count);
        LoadNextTopic();
        StartPuzzle();
        UpdateScore(0);
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
        if (!_lookupEnumerator.MoveNext())
        {
            var id = CommonResources.IdOf(CommonResources.Resource.DarulErkam, UserManager.CurrentLevel);
            UserManager.Instance.UnlockAchievement(id);
            EndOfLevel();
            return;
        }

        var current = _lookupEnumerator.Current;
        if (current == null) return;
        topicHead.text = string.Join("\n", current.Key.Split());
        var wordEnumerator = current.GetEnumerator();
        _map.LoadPuzzle(wordEnumerator);
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
        _winScreen.SetActive(true);
        _puzzleScreen.SetActive(false);
    }

    public void UpdateScore(int score)
    {
        Scoreboard.text = score.ToString();
    }
}