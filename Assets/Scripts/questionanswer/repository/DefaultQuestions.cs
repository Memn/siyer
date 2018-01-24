using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultQuestions
{

    private static string[] choicesTexts = { "A", "B", "C" };

    public static Question[] defaultQuestions = {   new Question("Question " + 0, choicesTexts),
													new Question("Question " + 1, choicesTexts),
													new Question("Question " + 2, choicesTexts) 
													};


}
