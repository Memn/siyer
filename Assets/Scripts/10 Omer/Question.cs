public class Question
{
    public string Text;
    public string[] Choices;
    public int _answer;

    public string Answer
    {
        get { return Choices[_answer]; }
    }
}