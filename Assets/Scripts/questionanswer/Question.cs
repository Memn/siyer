using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question
{


    public Question(string questionBodyText, string[] choiceTexts)
    {
        questionBody = questionBodyText;
        choices = choiceTexts;
    }

    public QuestionTypes type
    {
        get
        {
            return QuestionTypes.FILL_IN_THE_BLANKS;
        }
    }
    public string questionBody
    {
        get;
    }
    public string[] choices
    {
        get;
    }

}
