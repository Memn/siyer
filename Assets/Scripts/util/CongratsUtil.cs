using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CongratsUtil : MonoBehaviour
{
    public Sprite[] Success;
    public Sprite[] Fail;

    private static Sprite RandomR(IList<Sprite> sprites)
    {
        var r = Random.Range(0, sprites.Count);
        return sprites[r];
    }

    public void ShowSuccess(float f)
    {
        Show(f, Success);
    }

    public void ShowFail(float f)
    {
        Show(f, Fail);
    }

    private void Show(float f, IList<Sprite> success)
    {
        gameObject.SetActive(true);
        GetComponent<Image>().sprite = RandomR(success);
        if (f > 0) Invoke("CloseAfter", f);
        
    }

    private void CloseAfter()
    {
        gameObject.SetActive(false);
    }
}