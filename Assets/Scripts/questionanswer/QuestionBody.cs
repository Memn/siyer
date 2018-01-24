using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestionBody : MonoBehaviour
{


    public Question question;

    public abstract string questionBodyText
    {
        get;
    }
}
