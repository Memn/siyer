using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestionBody : MonoBehaviour
{

    public QuestionHandler handler;

    public List<GameObject> choices;
    public Question question
    {
        get
        {
            return handler.question;
        }
    }

    public abstract string questionBodyText
    {
        get;
    }

    internal abstract void Restore();
}
