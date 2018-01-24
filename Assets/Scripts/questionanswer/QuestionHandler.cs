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

    private void updateQuestion()
    {
        questionBody.GetComponent<Text>().text = question.questionBody;
        foreach (string choiceText in question.choices)
        {
            GameObject choice = Instantiate(choicePrefab, Vector3.zero, Quaternion.identity);
            choice.GetComponent<Text>().text = choiceText;
            choice.GetComponent<RectTransform>().SetParent(choices.transform);
            ChoiceDecorator.decorate(question.type, choice);

        }
        QuestionDecorator.decorate(question.type, questionBody);
    }
}
