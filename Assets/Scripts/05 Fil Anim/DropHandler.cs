using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropHandler : MonoBehaviour,IDropHandler {

	void IDropHandler.OnDrop(PointerEventData eventData)
	{
		var choice = DragHandler.itemBeingDragged;
		transform.parent.GetComponent<Quest>().Dropped(choice);
	}
}
