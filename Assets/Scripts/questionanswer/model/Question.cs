using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question
{
    internal readonly string answer;
    internal readonly int points;

    public Question(string questionBody, string[] choices, string answer, int points, QuestionTypes type)
    {
        this.questionBodyRaw = questionBody;
        this.choicesRaw = choices;
        this.answer = answer;
        this.points = points;
        this.type = type;
    }

    public QuestionTypes type {
		get;
		set;
    }
    public string questionBodyRaw
    {
		get;
		set;

	}
    public string[] choicesRaw

    {
		get;
		set;
    }

}
