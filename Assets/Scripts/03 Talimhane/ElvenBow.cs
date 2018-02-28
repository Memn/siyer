using JetBrains.Annotations;
using UnityEngine;

public class ElvenBow : MonoBehaviour
{
    public TalimhaneManager Manager;
    public GameObject Arrow;
    public GameObject ArrowPrefab;
    private GameObject _arrow;
    private TalimhaneMusicPlayer _musicPlayer;
    private Animator _animator;

    private bool _gameEnded;


    private void Start()
    {
        // create an arrow to shoot
        _animator = GetComponent<Animator>();
        _musicPlayer = FindObjectOfType<TalimhaneMusicPlayer>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (_gameEnded) return;

        if (!Manager.HasArrows())
        {
            _animator.Play("talimhane_out_of_arrows");
            _gameEnded = true;
            return;
        }

        // game is steered via mouse
        // (also works with touch on android)
        if (Input.GetMouseButtonDown(0))
        {
            _animator.SetTrigger("ready");
        }

        // ok, player released the mouse
        // (player released the touch on android)
        if (!Input.GetMouseButtonUp(0)) return;
        _animator.SetTrigger("pull");
        _musicPlayer.Play(TalimhaneMusicPlayer.AudioClips.StringPull);
    }

    [UsedImplicitly]
    private void ArrowReleased()
    {
//        Debug.Log("Arrow Released");
        _musicPlayer.Play(TalimhaneMusicPlayer.AudioClips.ArrowSwoosh);

        CreateArrow();
        ShootArrow();
    }

    private void CreateArrow()
    {
        _arrow = Instantiate(ArrowPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        _arrow.name = "arrow";

        _arrow.transform.position = Arrow.transform.position;
        _arrow.transform.rotation = Arrow.transform.rotation;
        _arrow.transform.localScale = Vector3.one * 0.3f;

        FindObjectOfType<FollowingCamera>().setTarget(_arrow.transform);
    }

    private void ShootArrow()
    {
        _arrow.AddComponent<ElvenArrow>();
        var rigid = _arrow.GetComponent<Rigidbody>();

        rigid.isKinematic = false;
        rigid.velocity = _arrow.transform.forward * 100;

        FindObjectOfType<TalimhaneCameraController>().ToggleCameras("shoot");
    }


    [UsedImplicitly]
    private void StringReleased()
    {
//        Debug.Log("String Released");
        _musicPlayer.Play(TalimhaneMusicPlayer.AudioClips.StringRelease);
    }
}