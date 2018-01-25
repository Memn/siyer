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

    public override string questionBodyText
    {
        get
        {
            return questionText;
        }
    }

    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        GameObject choice = FillInTheBlanksChoice.itemBeingDragged;
        // support for single blanks
        questionText = question.questionBodyRaw.Replace(blankEscape, choice.GetComponent<Text>().text);
        handler.UpdateChoice(choice.GetComponent<Text>().text);
        GetComponent<Text>().text = questionText;
    }

    internal override void Restore()
    {
        questionText = question.questionBodyRaw.Replace(blankEscape, blankLiteral);
    }
}
