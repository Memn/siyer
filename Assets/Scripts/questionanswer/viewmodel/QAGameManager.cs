using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QAGameManager : MonoBehaviour
{

    int score = 0;
    public GameObject questionObject;

    public Text scoreValue;
    public Text questionPointValue;

    private QuestionHandler questionHandler;
    void Start()
    {
        questionHandler = questionObject.GetComponent<QuestionHandler>();
        questionHandler.manager = this;
    }
     void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            FindObjectOfType<MenuManager>().LoadScene("MainMenu");
        }
    }
    public void Restore()
    {
        questionHandler.Restore();
    }
    public void Skip()
    {
        questionHandler.Skip();

    }
    public void Approve()
    {
        score += questionHandler.Approve();
        UpdateBoard();
    }

    private void UpdateBoard()
    {
        scoreValue.text = score.ToString();
        questionPointValue.text = questionHandler.question.points.ToString();
    }

    internal void ModelUpdated()
    {
        UpdateBoard();
    }
}
