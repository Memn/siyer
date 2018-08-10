using System.Collections.Generic;
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

    private float _start;
    public CongratsUtil Congrats;

    [UsedImplicitly]
    public void Init()
    {
        _questions = QuestionRepoHandler.Questions(UserManager.Game.Level);
        CreateAllQuestions();
        _enumerator = 0;
        _answeredCount = 0;
        UpdateCurrent();
        _start = Time.time;
    }

    private void CreateAllQuestions()
    {
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
        if (correct)
        {
            _score++;
            Congrats.ShowSuccess(2);
        }
        else
            Congrats.ShowFail(2);

        if (++_answeredCount != _questions.Count)
        {
            Invoke("Next", 3);
            return;
        }

        EndOfGame(_score >= 6);
    }

    private void EndOfGame(bool success)
    {
        if (success)
        {
            var duties = CommonResources.DutyOf(UserManager.Game.Level);
            var reward = duties.Find(duty => duty.Building == CommonResources.Building.Omer).Reward;
            
            if (!UserManager.Game.IsAchieved(reward))
            {
                var timer = Time.time - _start;
                UserManager.Reward(CommonResources.Building.Omer, (int) (_score * 50 - (timer / 10)));
            }

            // Bonus
            var bonus = CommonResources.Extras(CommonResources.Building.Omer);
            if (_score > 8 && !UserManager.Game.IsAchieved(bonus))
                UserManager.Instance.UnlockAchievement(bonus, 250);
        }

        Invoke("Back", 3);
    }

    public void Back()
    {
        SceneManagementUtil.Load(SceneManagementUtil.Scenes.AnaEkran);
    }
}