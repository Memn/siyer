﻿using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    public GameObject QuestionParent;


    public Button PrevButton;
    public Button NextButton;


    private int _enumerator;
    private List<Question> _questions;
    private int _answeredCount;
    private int _score;

    [UsedImplicitly]
    public void Init()
    {
        _questions = QuestionRepoHandler.Questions(UserManager.Game.Level);
        CreateAllQuestions();
        _enumerator = 0;
        _answeredCount = 0;
        UpdateCurrent();
    }

    private void CreateAllQuestions()
    {
        Debug.Log(QuestionParent.transform.childCount);
        var i = 0;
        foreach (Transform child in QuestionParent.transform)
        {
            var handler = child.GetComponent<QuestionHandler>();
            handler.Manager = this;
            if (_questions.Count > i)
            {
                handler.ShowQuestion(_questions[i++]);
            }
        }
    }


    private void UpdateCurrent()
    {
        // deactivate all
        var i = 0;
        foreach (Transform child in QuestionParent.transform)
        {
            child.gameObject.SetActive(i++ == _enumerator);
        }


        PrevButton.interactable = _enumerator > 0;
        NextButton.interactable = _enumerator < _questions.Count - 1;
    }

    [UsedImplicitly]
    public void Next()
    {
        _enumerator++;
        UpdateCurrent();
    }

    [UsedImplicitly]
    public void Prev()
    {
        _enumerator--;
        UpdateCurrent();
    }

    public void Answer(bool correct)
    {
        if (correct) _score += 10 * UserManager.Game.Level;
        if (++_answeredCount == _questions.Count)
        {
            Debug.Log("All questions are answered");
        }
    }
}