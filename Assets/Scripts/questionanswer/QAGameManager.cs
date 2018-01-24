using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QAGameManager : MonoBehaviour
{

    public GameObject question;
    private QuestionHandler questionHandler;
    void Start()
    {
        questionHandler = question.GetComponent<QuestionHandler>();

    }
    public void Restore()
    {
        questionHandler.Restore();
    }
    public void Skip()
    {

    }
    public void Approve()
    {

    }
}
