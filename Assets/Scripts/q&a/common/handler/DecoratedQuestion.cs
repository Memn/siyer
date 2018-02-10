using System;
using System.Collections.Generic;

internal class DecoratedQuestion
{

    private static string blankEscape = "${blank}";
    private static string blankLiteral = "___________";

    public string QuestionBodyText
    {
        get
        {
            switch (question.type)
            {
                case QuestionTypes.FILL_BLANKS:
                    return question.questionBodyRaw.Replace(blankEscape, blankLiteral);
                case QuestionTypes.MULTIPLE_CHOICE:
                    return question.questionBodyRaw;
                default:
                    return question.questionBodyRaw;

            }
        }
    }

    internal string Answer
    {
        get
        {
            switch (question.type)
            {
                case QuestionTypes.MULTIPLE_CHOICE:
                    return question.choicesRaw[question.answer];
                case QuestionTypes.FILL_BLANKS:
                    return question.choicesRaw[question.answer];
                case QuestionTypes.ESTIMATION:
                    return question.answer.ToString();
                default:
                    throw new NotImplementedException("Convertion of answer is not complete.");
            }
        }
    }

    internal int Points
    {
        get
        {
            return question.points;
        }
    }
    private Question question;
    internal QuestionTypes Type
    {
        get
        {
            return question.type;
        }
    }

    public string[] Choices
    {
        get
        {
            return question.choicesRaw;
        }
    }

    public DecoratedQuestion(Question question)
    {
        this.question = question;
    }


}