using UnityEngine;
using UnityEngine.UI;

public class QuestionHandler : MonoBehaviour
{
    public Text QuestionText;
    public GameObject ChoicesParent;
    public GameObject ChoicePrefab;

    private Question _question;

    public void ShowQuestion(Question question)
    {
        _question = question;
        QuestionText.text = question.Text;
        Util.ClearChildren(ChoicesParent.transform);
        Util.Load(ChoicesParent, ChoicePrefab, question.Choices, (choice, choiceText) =>
        {
            choice.GetComponentInChildren<Text>().text = choiceText;
            choice.GetComponent<Button>().onClick.AddListener(delegate { AnswerHandler(choice, choiceText); });
        });
    }

    private void AnswerHandler(GameObject choice, string answer)
    {
        foreach (Transform child in ChoicesParent.transform)
        {
            child.GetComponent<Button>().interactable = false;
        }

        if (_question.Answer == answer)
        {
            choice.GetComponent<Image>().color = Color.green;
            Debug.Log("Correct");
        }
        else
        {
            choice.GetComponent<Image>().color = Color.red;
            Debug.Log("False");
        }
    }
}