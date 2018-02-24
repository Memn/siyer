using UnityEngine;
using UnityEngine.SceneManagement;
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
            SceneManager.LoadScene("02 Siyer");
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