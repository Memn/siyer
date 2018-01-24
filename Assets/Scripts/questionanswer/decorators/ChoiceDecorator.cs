using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceDecorator
{

    public static void decorate(QuestionTypes type, GameObject choice)
    {
        switch (type)
        {
            case QuestionTypes.FILL_IN_THE_BLANKS:
                choice.AddComponent<FillInTheBlanksChoice>();
                break;
            default:
                throw new NotImplementedException();
        }
    }

}
