using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QAGameManager : MonoBehaviour
{

    int score = 0;
    public GameObject questionObject;

    public Text scoreValue;
    public Text questionPointValue;

    public GameObject questionBody;
    public GameObject choices;
    public GameObject choicePrefab;

    public Button ApproveButton;

    void Awake()
    {
        QuestionHandler.Instance.Init(this);
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
        if (QuestionHandler.Instance.Approve())
        {
            score += QuestionHandler.Instance.Points;
        }
        else
        {
            score -= QuestionHandler.Instance.Points;
        }
        UpdateBoard();
    }

    private void UpdateBoard()
    {
        scoreValue.text = score.ToString();
        questionPointValue.text = QuestionHandler.Instance.Points.ToString();
    }

    internal void ModelUpdated()
    {
        questionBody.GetComponent<Text>().text = QuestionHandler.Instance.QuestionBodyText;
        foreach (var choice in QuestionHandler.Instance.Choices)
        {
            choice.GetComponent<RectTransform>().SetParent(choices.transform);
            choice.GetComponent<RectTransform>().localScale = Vector3.one;
        }
        ApproveButton.interactable = false;
        UpdateBoard();
    }

    internal void ClearChoices()
    {
        foreach (Transform child in choices.transform)
        {
            Destroy(child.gameObject);
        }
    }

    internal void ChoiceChanged()
    {
        ApproveButton.interactable = true;
    }
}
