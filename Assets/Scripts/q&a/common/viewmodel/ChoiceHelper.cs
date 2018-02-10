using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ChoiceHelper : MonoBehaviour
{
    internal void CreateChoices(UnityAction onChoiceClick, UnityAction<BaseEventData> onDeselect, Transform choicesParent)
    {
        QuestionTypes type = QuestionHandler.Instance.Type;
		

        foreach (var choiceText in QuestionHandler.Instance.Choices)
        {
            GameObject choice = GetComponent<ChoiceCreationHelper>().CreateChoice(type, choiceText, onChoiceClick, onDeselect);
            choice.GetComponent<RectTransform>().SetParent(choicesParent);
            choice.GetComponent<RectTransform>().localScale = Vector3.one;
        }
    }
}


// () =>
//             {
//                 QuestionHandler.Instance.Choice = choiceText;
//                 choiceDeselected = false;
//                 approveButton.interactable = true;
//             }