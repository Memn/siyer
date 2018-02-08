using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleChoicesQuestionBody : QuestionBody
{

     private string questionText;

    public override string questionBodyText
    {
        get
        {
            return question.questionBodyRaw;
        }
    }

    internal override void Restore()
    {
		// nothing to do with the question body when restored.
    }
}
