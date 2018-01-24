using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionDecorator
{


    public static void decorate(QuestionTypes type, GameObject questionBody)
    {
        switch (type)
        {
            case QuestionTypes.FILL_IN_THE_BLANKS:
                questionBody.AddComponent<FillInTheBlanksQuestionBody>();
                break;
            default:
                throw new NotImplementedException();
        }

    }
}
