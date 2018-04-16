using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text TimerText;
    public GameObject PlayerControlButtons;

    [SerializeField] private int _totalInSeconds = 130;
    private bool _stop = false;

    private float _startTime;

    private void Start()
    {
        _startTime = Time.time;
    }


    private void Update()
    {
        if (_stop) return;

        var t = Time.time - _startTime;
        var remaining = _totalInSeconds - t;
        var mins = (int) (remaining / 60);
        var seconds = (int) (remaining % 60);

        if (mins == 0 && seconds == 0) TimeIsUp();

        TimerText.text = mins.ToString("D2") + " : " + seconds.ToString("D2");
    }

    private void TimeIsUp()
    {
        _stop = true;
        PlayerControlButtons.SetActive(false);
    }

    public void Stop()
    {
        _stop = true;
    }
}