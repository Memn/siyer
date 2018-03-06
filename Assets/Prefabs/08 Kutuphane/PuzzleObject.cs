using UnityEngine;

public class PuzzleObject : MonoBehaviour
{
    public char Letter { get; private set; }

    public void SetCharacter(char c)
    {
        var textMesh = transform.Find("Text").GetComponent<TextMesh>();
        Letter = c;
        textMesh.text = c.ToString();
    }
}