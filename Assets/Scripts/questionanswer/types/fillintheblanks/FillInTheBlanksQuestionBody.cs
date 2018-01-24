using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FillInTheBlanksQuestionBody : QuestionBody, IDropHandler
{
    private static string blankEscape = "${blank}";
    private static string blankLiteral = "___________";

    private string questionText;

    void Start()
    {
        questionText = question.questionBodyRaw.Replace(blankEscape, blankLiteral);
    }

    public override string questionBodyText
    {
        get
        {
            return questionText;
        }
    }

    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        string choiceText = FillInTheBlanksChoice.itemBeingDragged.GetComponent<Text>().text;
        questionText = ReplaceFirst(questionText, blankLiteral, choiceText);
        GetComponent<Text>().text = questionText;
    }

    private string ReplaceFirst(string text, string search, string replace)
    {
        int index = text.IndexOf(search);
        return index >= 0 ? text.Insert(index, @replace) : text;
    }
}
