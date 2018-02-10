using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionRepository : MonoBehaviour
{

    private static QuestionRepository _instance;

    public static QuestionRepository Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject handler = new GameObject("QuestionRepository");
                _instance = handler.AddComponent<QuestionRepository>();
            }
            return _instance;
        }
    }

    private static int number = 0;
    public Question next
    {
        get
        {
            return DefaultQuestions.defaultQuestions[number++ % DefaultQuestions.defaultQuestions.Length];
        }
    }


}
