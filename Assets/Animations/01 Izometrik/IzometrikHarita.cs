using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class IzometrikHarita : MonoBehaviour
{
    [SerializeField] private TextMesh _textMesh;
    [SerializeField] private Camera _camera;
    public GameObject Finger;

    private AudioSource _source;

    private enum Places
    {
        Medine,
        Kudus,
        Ebva,
        BeniSaad,
        Taif,
        Habesistan,
        Hira,
        Busra
    }

    private Dictionary<Places, string> _textDict;
    public GameObject InfoPanel;

    private void Start()
    {
        _source = GetComponent<AudioSource>();
        _textDict = new Dictionary<Places, string>
        {
            {Places.Medine, "Medine: Kur'an’ın kalan kısmının indirildiği yer,\n hicret yurdu"},
            {Places.Ebva, "Ebva: Efendimizin (s.a.v.) annesi,\n Hz. Âmine’nin vefat ettiği belde"},
            {
                Places.BeniSaad,
                "Beni Sa'd: Efendimiz (s.a.v.)  yaklaşık dört \nyıl kadar, burada sütannesinde kalmıştır"
            },
            {Places.Taif, "Taif: Peygamberimizin (s.a.v.) tebliğ için,\n memleketinden ilk uzaklaştığı yer"},
            {Places.Habesistan, "Habeşistan: Sahabilerin ilk hicret mekânı"},
            {Places.Hira, "Hira: Kur'an-ı Kerîm'in ilk \n ayetinin indirildiği mağara"},
            {
                Places.Busra,
                "Busra: Hem ticari hem dini bakımdan önemli olan\n Busra, Şam yolunda kervanların uğrak yeridir"
            },
            {Places.Kudus, "Kudüs: Efendimizin Miraca çıkarıldığı yer,\n Müslümanların ilk kıblesi"}
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
        var clipName = GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name;
        if (clipName != "Idle")
        {
            return;
        }

        Finger.SetActive(false);
        Invoke("ActivateFinger", 5.5f);
        switch (transformGameObject.name)
        {
            case "kabe":
                Finger.SetActive(true);
                EnterGame();
                break;
            case "kudus":
                GetComponent<Animator>().SetTrigger("kudus");
                break;
            case "medine":
                GetComponent<Animator>().SetTrigger("medine");
                break;
            case "benisaad":
                GetComponent<Animator>().SetTrigger("benisaad");
                break;
            case "hira":
                GetComponent<Animator>().SetTrigger("hira");
                break;
            case "ebva":
                GetComponent<Animator>().SetTrigger("ebva");
                break;
            case "taif":
                GetComponent<Animator>().SetTrigger("taif");
                break;
            case "habesistan":
                GetComponent<Animator>().SetTrigger("habesistan");
                break;
            case "busra":
                GetComponent<Animator>().SetTrigger("busra");
                break;
        }
    }

    public AudioClip Bismillah;
    public void EnterGame()
    {
        _source.Stop();
        _source.PlayOneShot(Bismillah);
        Invoke("Enter", 1);
    }

    private void Enter()
    {
        SceneManagementUtil.Load(SceneManagementUtil.Scenes.AnaEkran);
    }

    private void ActivateFinger()
    {
        Finger.SetActive(true);
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