using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MultipleChoiceManager : MonoBehaviour
{


    int score = 0;

    public Text scoreValue;
    public Text questionPointValue;
    public Text QuestionText;

    private Choices Choice;
    public Button ApproveButton;

    private bool choiceDeselected;

    void Start()
    {
        choiceDeselected = false;
        ApproveButton.interactable = false;
    }


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
        Debug.Log("Approved:");
        Debug.Log(Choice);
        //TODO: Win-Lose cases 
        // if (QuestionHandler.Instance.Approve())
        // {
        //     score += QuestionHandler.Instance.Points;
        // }
        // else
        // {
        //     score -= QuestionHandler.Instance.Points;
        // }
        UpdateBoard();
    }

    private void UpdateBoard()
    {
        scoreValue.text = score.ToString();
        questionPointValue.text = QuestionHandler.Instance.Points.ToString();
    }

    internal void ModelUpdated()
    {
        QuestionText.text = QuestionHandler.Instance.QuestionBodyText;
        ApproveButton.interactable = false;
        UpdateBoard();
    }

    public void SetChoice(string choice)
    {
        choiceDeselected = false;
        Choice = (Choices)Enum.Parse(typeof(Choices), choice);
        ApproveButton.interactable = true; ;
    }
    public void DeselectChoice()
    {
        choiceDeselected = true;
        Invoke("ControlApprove", 0.2f);
    }
    void ControlApprove()
    {
        if (choiceDeselected)
        {
            Choice = Choices.None;
            ApproveButton.interactable = false;
        }

    }

}
