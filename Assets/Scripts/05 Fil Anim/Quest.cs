using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Quest : MonoBehaviour
{
    public VideoClip VideoClip;
    public bool FillInTheBlanks;

    private Image _image;


    private const string BlankEscape = "${blank}";
    private const string BlankLiteral = "...............";

    private Text _questionText;
    private string _originalQuestionText;
    [SerializeField] private int _answer;
    public bool HasQuestion = true;
    private GameObject _choices;

    public bool Completed;


    private void Start()
    {
        Answered = false;
        _choices = transform.Find("Choices").gameObject;
        DecorateQuestion();
    }

    private void DecorateQuestion()
    {
        if (!FillInTheBlanks) return;

        foreach (Transform child in transform)
        {
            if (child.CompareTag("QuestionText"))
            {
                child.gameObject.AddComponent<DropHandler>();
                _questionText = child.GetComponent<Text>();
                _originalQuestionText = _questionText.text;
                _questionText.text = _originalQuestionText.Replace(BlankEscape, BlankLiteral);
            }
            else if (child.transform.name.Equals("Choices"))
            {
                foreach (Transform choice in child.transform)
                {
                    choice.gameObject.AddComponent<DragHandler>();
                    choice.gameObject.AddComponent<CanvasGroup>();
                }
            }
        }
    }


    public bool isVideo
    {
        get { return VideoClip != null; }
    }

    public bool Answered { get; private set; }

    public CongratsUtil Congrats;

    public void Answer(int choice)
    {
        Answered = true;
        for (var i = 0; i < _choices.transform.childCount; i++)
        {
            var t = _choices.transform.GetChild(i).gameObject;
            if (i != choice)
                t.GetComponent<Button>().interactable = false;
            else if (_answer == choice)
            {
                t.GetComponent<Image>().color = Color.green;
                Congrats.ShowSuccess(2);
                Invoke("Close",3);
            }
            else
            {
                t.GetComponent<Image>().color = Color.red;
                Congrats.ShowFail(2);
            }
        }
    }

    private void Close()
    {
        var qc = FindObjectOfType<QuestsController>();
        if (qc && gameObject.activeSelf) qc.CloseQuestion();
    }

    public void Dropped(GameObject choice)
    {
        Answer(choice.transform.GetSiblingIndex());
        var text = choice.transform.Find("Text").GetComponent<Text>();
        _questionText.text = _originalQuestionText.Replace(BlankEscape, text.text);
        Invoke("RemoveDragging", 0.5f);
    }

    private void RemoveDragging()
    {
        foreach (var handler in _choices.GetComponentsInChildren<DragHandler>())
            Destroy(handler);
    }
}