using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultQuestions
{

    private static string[] choicesTexts = { "A", "B", "C" };
    private static string q1 = "Question ${blank} some questionary?";

    private static string q2 = "Question ${blank} some ${blank} questionary?";
    private static string q3 = "Question ${blank} some  ${blank} another ${blank} questionary?";
    public static Question[] defaultQuestions = {
        new Question(q1, choicesTexts, QuestionTypes.FILL_IN_THE_BLANKS),
        new Question(q2, choicesTexts, QuestionTypes.FILL_IN_THE_BLANKS),
        new Question(q3, choicesTexts, QuestionTypes.FILL_IN_THE_BLANKS)
        };


}
