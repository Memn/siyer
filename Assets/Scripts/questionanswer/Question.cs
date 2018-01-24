using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question
{

    public Question(string questionBody, string[] choices, QuestionTypes type)
    {
        this.questionBodyRaw = questionBody;
        this.choicesRaw = choices;
        this.type = type;
    }

    public QuestionTypes type
    {
        get;
    }
    public string questionBodyRaw
    {
        get;
    }
    public string[] choicesRaw

    {
        get;
    }

}
