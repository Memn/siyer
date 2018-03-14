using UnityEngine;
using UnityEngine.UI;

public class TalimhaneManager : MonoBehaviour
{
    [SerializeField] private Text _arrows;
    [SerializeField] private Text _score;
    [SerializeField] private Text _distance;

    public int Arrows = 30;
    private int _points;

    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _target;
    [SerializeField] private GameObject _aim;

    private TalimhaneMusicPlayer _musicPlayer;

    private void Start()
    {
        _musicPlayer = FindObjectOfType<TalimhaneMusicPlayer>();
        RecalculateDistance();
    }

    public void RecalculateDistance()
    {
        _distance.text = CalculateDistance();
        _aim.GetComponent<AimDrawer>()
            .AdjustAccordingToDistance(Vector3.Distance(_player.transform.position, _target.transform.position));
    }

    private string CalculateDistance()
    {
        var distance = Vector3.Distance(_player.transform.position, _target.transform.position);
        return distance.ToString("F2") + "m";
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManagementUtil.Load(SceneManagementUtil.Scenes.AnaEkran);
        }
    }

    public bool HasArrows()
    {
        return Arrows > 0;
    }

    public void Hit()
    {
        _musicPlayer.Play(TalimhaneMusicPlayer.AudioClips.ArrowImpact);
        Arrows--;
        _arrows.text = Arrows.ToString();
        _points += 10;
        _score.text = _points.ToString();
    }

    public void NotHit()
    {
        Arrows--;
        _arrows.text = Arrows.ToString();
        _points += 0;
        _score.text = _points.ToString();
    }
}