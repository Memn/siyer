using UnityEngine;
using UnityEngine.UI;

public class MazeManager : MonoBehaviour
{
    private int _remaining;
    private int _collected;
    public Text CollectedText;
    public Text RemainingText;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManagementUtil.Load(SceneManagementUtil.Scenes.AnaEkran);
        }
    }

    internal void CoinCollected()
    {
        _collected++;
        _remaining--;
        CollectedText.text = _collected.ToString();
        RemainingText.text = _remaining.ToString();
    }

    internal void SetRemaining(int v)
    {
        _remaining = v;
        RemainingText.text = _remaining.ToString();
    }
}