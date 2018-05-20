using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class TalimhaneManager : MonoBehaviour
{
    [SerializeField] private Text _arrows;
    [SerializeField] private Text _score;
    [SerializeField] private Text _distance;

    public int Arrows = 10;
    private int _points;

    [SerializeField] private GameObject _player;

    [SerializeField] private Animator _targetAnimator;
    [SerializeField] private Animator _strawAnimator;

    private TalimhaneMusicPlayer _musicPlayer;

    private void Start()
    {
        _musicPlayer = FindObjectOfType<TalimhaneMusicPlayer>();
        _arrows.text = Arrows.ToString();
        _score.text = _points.ToString();
        RecalculateDistance();
    }

    public void RecalculateDistance()
    {
        _distance.text = CalculateDistance();
    }

    private string CalculateDistance()
    {
        var targeTransformPos = FindObjectOfType<TalimhaneTarget>().transform.position;
        var distance = Vector3.Distance(_player.transform.position, targeTransformPos);
        return distance.ToString("F2") + "m";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Back();
        }
    }

    [UsedImplicitly]
    public void Back()
    {
        SceneManagementUtil.Load(SceneManagementUtil.Scenes.AnaEkran);
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

    public void SetTrigger(string triggerName)
    {
        if (triggerName == "hit")
            _strawAnimator.SetTrigger(triggerName);
        else
            _targetAnimator.SetTrigger(triggerName);
    }

    public void MoveTarget(bool straw)
    {
        var hitTargetAnimator = straw ? _strawAnimator : _targetAnimator;
        hitTargetAnimator.transform.parent.GetComponent<TalimhaneTarget>().SetPostion();
    }
}