using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionRepository
{

    private static int number = 0;
    private static int max = 3;
    public static Question next
    {
        get
        {
            return DefaultQuestions.defaultQuestions[number++ % max];
        }
    }


}
