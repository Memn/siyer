using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class ChoiceCreationHelper : MonoBehaviour
{

    public GameObject MultipleChoicePrefab;

    public GameObject CreateChoice(QuestionTypes type, string item, UnityAction onClick, UnityAction<BaseEventData> deselect)
    {
        GameObject choice;
        switch (type)
        {
            case QuestionTypes.MULTIPLE_CHOICE:
                choice = Instantiate(MultipleChoicePrefab, Vector3.zero, Quaternion.identity);
                choice.GetComponent<Button>().onClick.AddListener(onClick);
                AddDeselectTrigger(choice, deselect);
                choice.transform.Find("ChoiceLabel").GetComponent<Text>().text = item;
                return choice;
            case QuestionTypes.FILL_BLANKS:
                choice = Instantiate(MultipleChoicePrefab, Vector3.zero, Quaternion.identity);
                choice.GetComponent<Button>().onClick.AddListener(onClick);
                AddDeselectTrigger(choice, deselect);
                choice.transform.Find("ChoiceLabel").GetComponent<Text>().text = item;
                return choice;
            default:
                return null;
        }
    }
    private void AddDeselectTrigger(GameObject choice, UnityAction<BaseEventData> deselect)
    {
        EventTrigger.Entry triggerEntry = new EventTrigger.Entry();
        triggerEntry.eventID = EventTriggerType.Deselect;
        choice.GetComponent<EventTrigger>().triggers.Add(triggerEntry);
        triggerEntry.callback.AddListener(deselect);
    }


}
