using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class MazeManager : MonoBehaviour
{
    private int _remaining;
    private int _collected;
    public Text CollectedText;
    public Text RemainingText;

    public Timer Timer;
    public GameObject PlayerControlButtons;

    public GameObject Congrats;

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

    internal void CoinCollected()
    {
        _collected++;
        _remaining--;
        CollectedText.text = _collected.ToString();
        RemainingText.text = _remaining.ToString();
        if (_remaining == 0) EndOfGame(true);
    }

    public void EndOfGame(bool success)
    {
        Timer.Stop();
        PlayerControlButtons.SetActive(false);
        if (success)
        {
            UserManager.MazeSuccess(_collected, Timer.Remaining);
            Congrats.GetComponent<CongratsUtil>().ShowSuccess(-1);
            Invoke("Back", 5);
        }
        else
        {
            Congrats.GetComponent<CongratsUtil>().ShowFail(-1);
            Invoke("Back", 5);
        }
    }

    internal void SetRemaining(int v)
    {
        _remaining = v;
        RemainingText.text = _remaining.ToString();
    }
}