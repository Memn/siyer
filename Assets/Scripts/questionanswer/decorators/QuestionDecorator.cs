using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionDecorator
{


    internal static QuestionBody decorate(QuestionHandler questionHandler)
    {
        switch (questionHandler.question.type)
        {
            case QuestionTypes.FILL_IN_THE_BLANKS:
                return CreateComponent<FillInTheBlanksQuestionBody>(questionHandler.manager.questionBody, questionHandler);

            default:
                throw new NotImplementedException();
        }
    }
    public static T CreateComponent<T>(GameObject where, QuestionHandler handler) where T : QuestionBody
    {
        T myC = where.AddComponent<T>();
        myC.handler = handler;
        return myC;

    }

}
