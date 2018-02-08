using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionHandler : MonoBehaviour
{
    private static QuestionHandler _instance;
    public static QuestionHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject handler = new GameObject("QuestionHandler");
                _instance = handler.AddComponent<QuestionHandler>();
            }
            return _instance;
        }
    }

    public string QuestionBodyText
    {
        get
        {
            return decorated.questionBodyText;
        }
    }
    internal void Init(QAGameManager qAGameManager)
    {
        manager = qAGameManager;
        choicePrefab = qAGameManager.choicePrefab;
    }

    public int Points
    {
        get
        {
            return question.points;
        }
    }
    public List<GameObject> Choices = new List<GameObject>();
    public Question question;

    private GameObject choicePrefab;
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
        question = QuestionRepository.Instance.next;
        CreateQuestion();
        Restore();
    }

    private void CreateQuestion()
    {
        decorated = QuestionDecorator.decorate(this);
        Choices.Clear();
        manager.ClearChoices();
        CreateChoices();
    }

    private void CreateChoices()
    {
        foreach (string rawChoiceText in question.choicesRaw)
        {
            GameObject choice = Instantiate(choicePrefab, Vector3.zero, Quaternion.identity);
            choice.GetComponent<Choice>().raw = rawChoiceText;
            ChoiceDecorator.decorate(question.type, choice);
            choice.GetComponent<Text>().text = choice.GetComponent<Choice>().text;
            Choices.Add(choice);
        }
    }
    internal void Restore()
    {
        choice = null;
        decorated.Restore();
        manager.ModelUpdated();
    }

    internal bool Approve()
    {
        if (choice != null)
        {
            if (question.answer == choice)
            {
                return true;
            }
        }
        return false;
    }

    internal void UpdateChoice(string choice)
    {
        this.choice = choice;
        manager.ChoiceChanged();
    }

}
