using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultQuestions
{

    private static string[] choicesTexts = { "A", "B", "C" };
    private static string q1 = "Basic question ${blank} some questionary?";

    private static string q2 = "Medium Question  some ${blank} questionary?";
    private static string q3 = "Hard Question  some  another ${blank} questionary?";
    public static Question[] defaultQuestions = {
        new Question(q1, choicesTexts, "B", 10, QuestionTypes.FILL_IN_THE_BLANKS),
        new Question(q2, choicesTexts, "A", 15, QuestionTypes.FILL_IN_THE_BLANKS),
        new Question(q3, choicesTexts, "C", 20, QuestionTypes.FILL_IN_THE_BLANKS)
        };


}
