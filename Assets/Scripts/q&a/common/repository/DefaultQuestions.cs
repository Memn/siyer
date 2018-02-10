using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultQuestions
{

    private static string[] choicesTexts = { "A", "B", "C" };
    private static string[] trueFalse = { "Doğru", "Yanlış" };
    private static string q1 = "Basic question ${blank} some questionary?";

    private static string q2 = "Medium Question  some ${blank} questionary?";
    private static string q3 = "Hard Question  some  another ${blank} questionary?";
    private static string q4 = "Some Multiple Choice Question?";
    private static string q5 = "An Estimation Question 200?";
    private static string q6 = "Another Estimation Question 400?";
    private static string q7 = "True or False?";

    public static Question[] defaultQuestions = {
        new Question(q1, choicesTexts, 1, 10, QuestionTypes.FILL_BLANKS),
        new Question(q2, choicesTexts, 0, 15, QuestionTypes.FILL_BLANKS),
        new Question(q3, choicesTexts, 2, 20, QuestionTypes.FILL_BLANKS),
        new Question(q4, choicesTexts, 1, 20, QuestionTypes.MULTIPLE_CHOICE),
        new Question(q5, null, 200, 20, QuestionTypes.ESTIMATION),
        new Question(q6, null, 400, 20, QuestionTypes.ESTIMATION),
        new Question(q7, trueFalse, 0, 30, QuestionTypes.MULTIPLE_CHOICE)

        };


}
