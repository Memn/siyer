using UnityEngine;
using UnityEngine.UI;

public class AnswerHandler : MonoBehaviour {

	public void Right()
	{
		GetComponent<Image>().color = Color.green;
	}
	public void Wrong()
	{
		GetComponent<Image>().color = Color.red;
	}
}
