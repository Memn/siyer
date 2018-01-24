using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FillInTheBlanksQuestionBody : MonoBehaviour, IDropHandler
{


    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        GetComponent<Text>().text = FillInTheBlanksChoice.itemBeingDragged.GetComponent<Text>().text;

    }
}
