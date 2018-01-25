using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionHandler : MonoBehaviour
{

    public Question question;
    public GameObject questionBody;
    public GameObject choices;
    public GameObject choicePrefab;

    private QuestionBody decorated;
    internal QAGameManager manager;
    private string choice;

    void Start()
    {
        // works for first too.
        Skip();
    }

    internal void Skip()
    {
        question = QuestionRepository.next;
        CreateQuestion();
        Restore();
    }

    private void CreateQuestion()
    {
        decorated = QuestionDecorator.decorate(this);

        foreach (Transform child in choices.transform)
        {
            Destroy(child.gameObject);
        }

        CreateChoices();
    }

    private void CreateChoices()
    {
        foreach (string rawChoiceText in question.choicesRaw)
        {
            GameObject choice = Instantiate(choicePrefab, Vector3.zero, Quaternion.identity);
            choice.GetComponent<Choice>().raw = rawChoiceText;
            choice.GetComponent<RectTransform>().SetParent(choices.transform);
            ChoiceDecorator.decorate(question.type, choice);
            choice.GetComponent<Text>().text = choice.GetComponent<Choice>().text;
        }
    }
    internal void Restore()
    {
        choice = null;
        decorated.Restore();
        UpdateView();
    }

    private void UpdateView()
    {
        questionBody.GetComponent<Text>().text = decorated.questionBodyText;
        manager.ModelUpdated();
    }

    internal int Approve()
    {
        if (choice != null)
        {
            if (question.answer == choice)
            {
                return question.points;
            }
        }
        return -1;
    }

    internal void UpdateChoice(string choice)
    {
        this.choice = choice;
    }

}
