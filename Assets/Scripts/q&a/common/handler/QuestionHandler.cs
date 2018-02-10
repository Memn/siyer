using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionHandler : MonoBehaviour
{
    // Singleton instance
    private static QuestionHandler _instance;
    public static QuestionHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject handler = new GameObject("QuestionHandler");
                _instance = handler.AddComponent<QuestionHandler>();
            }
            return _instance;
        }
    }
    private DecoratedQuestion decorated;
    public string QuestionBodyText
    {
        get
        {
            return decorated.QuestionBodyText;
        }
    }
    public int Points
    {
        get
        {
            return decorated.Points;
        }
    }

    // private variables
    internal QAManager manager;
    internal string[] Choices{
        get{
            return decorated.Choices;
        }
    }
    internal QuestionTypes Type{
        get{
            return decorated.Type;
        }
    }

    public string Choice { get; internal set; }

    internal void Init(QAManager qAGameManager)
    {
        manager = qAGameManager;
    }

    // actions
    internal void Skip()
    {
        decorated = new DecoratedQuestion(QuestionRepository.Instance.next);
        Restore();
    }

    internal void Restore()
    {
        Choice = "";
        manager.ModelUpdated();
    }

    internal bool CheckAnswer()
    {
        return Choice == decorated.Answer;
    }
}
