using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionHandler : MonoBehaviour
{

    private Question question;
    public GameObject questionBody;
    public GameObject choices;
    public GameObject choicePrefab;


    // Use this for initialization
    void Start()
    {
        question = QuestionRepository.next;
        updateQuestion();
    }

    internal void Restore()
    {
        questionBody.GetComponent<Text>().text = questionBody.GetComponent<QuestionBody>().questionBodyText;
    }

    private void updateQuestion()
    {
        QuestionBody decorated = QuestionDecorator.decorate(question, questionBody);
        questionBody.GetComponent<Text>().text = decorated.questionBodyText;

        foreach (string rawChoiceText in question.choicesRaw)
        {
            GameObject choice = Instantiate(choicePrefab, Vector3.zero, Quaternion.identity);
            choice.GetComponent<Choice>().raw = rawChoiceText;
            choice.GetComponent<Text>().text = choice.GetComponent<Choice>().text;
            choice.GetComponent<RectTransform>().SetParent(choices.transform);
            ChoiceDecorator.decorate(question.type, choice);
        }
    }

}
