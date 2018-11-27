using System;
using System.Linq;

[Serializable]
public class Question
{
    public string Text;
    public string[] Choices;
    public int _answer;
    public bool Answered;


    public string Answer
    {
        get { return Choices.Length == 0 ? "" : Choices[_answer]; }
    }

    public bool Sync(Question question)
    {
        var sync = true;
        if (Text != question.Text)
        {
            LogUtil.Log("Question Text are not same!");
            LogUtil.Log("Text: " + Text + " replacement: " + question.Text);
            Text = question.Text;
            sync = false;
        }

        if (_answer != question._answer)
        {
            LogUtil.Log("Question Answer are not same!");
            LogUtil.Log("Answer: " + _answer + " replacement: " + question._answer);
            _answer = question._answer;
            sync = false;
        }

        if (Choices.SequenceEqual(question.Choices)) return sync;
        LogUtil.Log("Question Choices are not same!");
        LogUtil.Log("Choices: " + Choices + " replacement: " + question.Choices);
        Choices = question.Choices;
        return false;
    }
}