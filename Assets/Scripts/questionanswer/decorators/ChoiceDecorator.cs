using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceDecorator : MonoBehaviour
{

    public static void decorate(QuestionTypes type, GameObject choice)
    {
        switch (type)
        {
            case QuestionTypes.FILL_IN_THE_BLANKS:
                choice.AddComponent<FillInTheBlanksChoice>();
                break;
            case QuestionTypes.MULTIPLE_CHOICES:
                choice.AddComponent<MultipleChoicesChoice>();
                break;
            default:
                throw new NotImplementedException();
        }
    }

}
