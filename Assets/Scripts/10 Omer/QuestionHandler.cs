using UnityEngine;
using UnityEngine.UI;

public class QuestionHandler : MonoBehaviour
{
    public Text QuestionText;
    public GameObject ChoicesParent;
    public GameObject ChoicePrefab;
    internal QuestionManager Manager;
    private string _questionAnswer;


    public void ShowQuestion(Question question)
    {
        _questionAnswer = question.Answer;
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

        if (_questionAnswer == answer)
        {
            choice.GetComponent<Image>().color = Color.green;
            Debug.Log("Correct");
            Manager.Answer(true);
        }
        else
        {
            choice.GetComponent<Image>().color = Color.red;
            Debug.Log("False");
            Manager.Answer(false);
        }
    }
}