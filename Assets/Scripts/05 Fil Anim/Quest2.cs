using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class Quest2 : MonoBehaviour
{
    public string Url;
    internal Question Question;
    internal bool Completed;
    public CongratsUtil Congrats;


    internal bool VideoClipAvailable
    {
        get { return File.Exists(VideoLocation); }
    }

    internal string VideoLocation
    {
        get
        {
            var filename = Util.QuestName() + gameObject.name + ".mp4";
            return Path.Combine(Application.persistentDataPath, filename);
        }
    }

    public bool IsQuestionActive
    {
        get { return Question != null && Question.Text != "" && !Question.Answered; }
    }

    internal void DecorateQuestion()
    {
        if (!IsQuestionActive) return;
        transform.Find("QuestionText").GetComponent<Text>().text = Question.Text;
        var choices = transform.Find("Choices");
        var choicePrefab = choices.transform.Find("Choice");
        for (var i = 0; i < Question.Choices.Length - 1; i++)
        {
            Instantiate(choicePrefab, Vector3.zero, Quaternion.identity, choices);
        }

        var index = 0;
        foreach (Transform choice in choices)
        {
            choice.name = string.Format("Choice({0})", index);
            choice.GetComponentInChildren<Text>().text = Question.Choices[index];
            var ah = choice.gameObject.AddComponent<AnswerHandler>();
            ah.Congrats = Congrats;
            choice.gameObject.GetComponent<Button>().onClick.AddListener(() => CheckAnswer(choice));
            index++;
        }
    }

    private void CheckAnswer(Component choice)
    {
        var index = int.Parse(choice.name.Substring(7, choice.name.Length - 8));

        if (index == Question._answer)
        {
            choice.GetComponent<AnswerHandler>().Right();
            UserManager.ReportScore(50);
        }
        else
            choice.GetComponent<AnswerHandler>().Wrong();

        foreach (var button in choice.transform.parent.gameObject.GetComponentsInChildren<Button>())
        {
            button.interactable = false;
        }

        Question.Answered = true;
    }
}