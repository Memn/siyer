using UnityEngine;
using UnityEngine.UI;

public class AnswerHandler : MonoBehaviour
{
    public CongratsUtil Congrats;


    public void Right()
    {
        GetComponent<Image>().color = Color.green;
        Congrats.ShowSuccess(2, () => Invoke("BackToGame", 2));
    }

    public void Wrong()
    {
        GetComponent<Image>().color = Color.red;
        Congrats.ShowFail(2, () => Invoke("BackToGame", 2));
    }

    private void BackToGame()
    {
        FindObjectOfType<QuestsController>().CloseQuestion();
    }
}