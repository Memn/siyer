using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class QAManager : MonoBehaviour
{

    public GameObject MainPanel;

    private int score = 0;
    protected bool choiceDeselected;

    // private instances
    private Text scoreValue;
    private Text questionPointValue;
    private Text questionText;
    private Button approveButton;
    private GameObject choicesParent;

    private string Choice;

    // Use this for initialization
    void Start()
    {
        scoreValue = MainPanel.transform
                    .Find("Board")
                    .Find("Value").GetComponent<Text>();
        questionPointValue = MainPanel.transform
                    .Find("Question")
                    .Find("Question Point")
                    .Find("Value").GetComponent<Text>();
        questionText = MainPanel.transform
                    .Find("Question")
                    .Find("QBody")
                    .Find("Question Text").GetComponent<Text>();
        approveButton = MainPanel.transform
                    .Find("Question")
                    .Find("Action")
                    .Find("Approve").GetComponent<Button>();
        choicesParent = MainPanel.transform
                    .Find("Question")
                    .Find("QBody")
                    .Find("Choices").gameObject;

        choiceDeselected = false;
        approveButton.interactable = false;
        QuestionHandler.Instance.Init(this);
        QuestionHandler.Instance.Skip();
    }

    // listen back key.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("02 Siyer");
        }
    }

    public void Restore()
    {
        QuestionHandler.Instance.Restore();
    }
    public void Skip()
    {
        QuestionHandler.Instance.Skip();
    }
    public void Approve()
    {
        choiceDeselected = false;
        if (QuestionHandler.Instance.CheckAnswer())
        {
            score += QuestionHandler.Instance.Points;
        }
        else
        {
            score -= QuestionHandler.Instance.Points;
        }
        UpdateBoard();
        Skip();
    }

    public void ModelUpdated()
    {
        questionText.text = QuestionHandler.Instance.QuestionBodyText;
        ClearChoices();
        CreateAndAddChoices();
        approveButton.interactable = false;
        UpdateBoard();
    }
    private void ClearChoices()
    {
        foreach (Transform child in choicesParent.transform)
        {
            Destroy(child.gameObject);
        }
    }


    public GameObject MultipleChoicePrefab;

    public GameObject EstimationChoicePrefab;
    private void CreateAndAddChoices()
    {
        QuestionTypes type = QuestionHandler.Instance.Type;
        switch (type)
        {
            case QuestionTypes.MULTIPLE_CHOICE:
                AddMultipleChoices();
                break;
            case QuestionTypes.FILL_BLANKS:
                AddMultipleChoices();
                break;
            case QuestionTypes.ESTIMATION:
                AddEstimationChoice();
                break;
            default:
                break;
        }
    }

    private void AddEstimationChoice()
    {
        GameObject choice = Instantiate(EstimationChoicePrefab, Vector3.zero, Quaternion.identity);
        choice.GetComponent<RectTransform>().SetParent(choicesParent.transform);
        choice.GetComponent<RectTransform>().localScale = Vector3.one;
        choice.GetComponent<InputField>().onValueChanged.AddListener((input) => QuestionHandler.Instance.Choice = input);
        choice.GetComponent<InputField>().onValueChanged.AddListener((input) => approveButton.interactable = input.Trim().Length > 0);
    }

    private void AddMultipleChoices()
    {
        foreach (var choiceText in QuestionHandler.Instance.Choices)
        {
            GameObject choice = Instantiate(MultipleChoicePrefab, Vector3.zero, Quaternion.identity);
            choice.GetComponent<Button>().onClick.AddListener(() =>
            {

                QuestionHandler.Instance.Choice = choiceText;
                choiceDeselected = false;
                approveButton.interactable = true;
            });
            AddDeselectTrigger(choice, DeselectChoice);
            choice.transform.Find("ChoiceLabel").GetComponent<Text>().text = choiceText;
            choice.GetComponent<RectTransform>().SetParent(choicesParent.transform);
            choice.GetComponent<RectTransform>().localScale = Vector3.one;
        }
    }

    private void AddDeselectTrigger(GameObject choice, UnityAction<BaseEventData> deselect)
    {
        EventTrigger.Entry triggerEntry = new EventTrigger.Entry();
        triggerEntry.eventID = EventTriggerType.Deselect;
        choice.GetComponent<EventTrigger>().triggers.Add(triggerEntry);
        triggerEntry.callback.AddListener(deselect);
    }
    private void DeselectChoice(BaseEventData arg0)
    {
        choiceDeselected = true;
        Invoke("ControlApprove", 0.2f);
    }

    private void UpdateBoard()
    {
        scoreValue.text = score.ToString();
        questionPointValue.text = QuestionHandler.Instance.Points.ToString();
    }

    private void ControlApprove()
    {
        if (choiceDeselected)
        {
            approveButton.interactable = false;
        }
    }
}
