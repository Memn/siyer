using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestionBody : MonoBehaviour, IDropHandler
{
    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        print("change the text with: ");
		GetComponent<Text>().text = Choice.itemBeingDragged.GetComponent<Text>().text;

    }
}
