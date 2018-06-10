using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CongratsUtil : MonoBehaviour
{
    public Sprite[] Success;
    public Sprite[] Fail;
    public AudioClip SuccessClip;
    public AudioClip FailClip;
    public GameObject Back;
    

    private static Sprite RandomR(IList<Sprite> sprites)
    {
        var r = Random.Range(0, sprites.Count);
        return sprites[r];
    }

    public void ShowSuccess(float f)
    {
        Show(f, Success);
        GetComponent<AudioSource>().PlayOneShot(SuccessClip);
    }

    public void ShowFail(float f)
    {
        Show(f, Fail);
        GetComponent<AudioSource>().PlayOneShot(FailClip);
    }

    private void Show(float f, IList<Sprite> success)
    {
        Back.SetActive(true);
        gameObject.SetActive(true);
        GetComponent<Image>().sprite = RandomR(success);
        if (f > 0) Invoke("CloseAfter", f);
        
    }

    private void CloseAfter()
    {
        Back.SetActive(false);
        gameObject.SetActive(false);
    }

    public void Deactivate()
    {
        CloseAfter();
    }
}