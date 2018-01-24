using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionDecorator
{


    public static QuestionBody decorate(Question question, GameObject questionBody)
    {
        switch (question.type)
        {
            case QuestionTypes.FILL_IN_THE_BLANKS:
                return CreateComponent<FillInTheBlanksQuestionBody>(questionBody, question);

            default:
                throw new NotImplementedException();
        }

    }
    public static T CreateComponent<T>(GameObject where, Question question) where T : QuestionBody
    {
        T myC = where.AddComponent<T>();
        myC.question = question;
        return myC;

    }
}
