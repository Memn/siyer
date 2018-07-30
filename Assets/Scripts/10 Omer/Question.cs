using System;

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
}