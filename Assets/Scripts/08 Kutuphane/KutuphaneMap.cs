using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class KutuphaneMap : MonoBehaviour
{
    public GameObject PuzzleObject;
    public Transform PuzzleParentTransform;
    private int _width = 5;
    private int _height = 5;
    private const float ScalingFactor = 0.8f;

    private List<string> _words;

    public void StartPuzzle(string word)
    {
        _words = word.ToUpper(CultureInfo.CurrentCulture).Split(' ').ToList();
        foreach (Transform child in PuzzleParentTransform)
        {
            Destroy(child.gameObject);
        }

        CreatePuzzle(Regex.Replace(word.ToUpper(CultureInfo.CurrentCulture), @"\s+", ""));
    }

    private void CreatePuzzle(string word)
    {
        var puzzle = PuzzleMaker.MakePuzzle(word);
        var startY = -1 * puzzle.height / 2;
        var startX = -1 * puzzle.width / 2;
        var start = new Vector2(startX, startY);
        for (var x = 0; x < puzzle.width; x++)
        {
            for (var y = 0; y < puzzle.height; y++)
            {
                var fy = start.y + y;
                var fx = start.x + x;

                if (y % 2 == 0)
                {
                    fx += 0.5f;
                }

                var pos = new Vector2(fx, fy);
                var go = Instantiate(PuzzleObject, Vector3.zero, Quaternion.identity);
                go.GetComponent<PuzzleObject>().SetCharacter(puzzle.puzzleData[x, y]);
                go.transform.parent = PuzzleParentTransform;
                go.transform.localPosition = pos;
                go.transform.localScale = Vector3.one * ScalingFactor;
            }
        }
    }


    public bool CheckAnswer(string word)
    {
        if (!_words.Contains(word)) return false;
        _words.Remove(word);
        return true;
    }

    public bool AnyWordsLeft()
    {
        return _words.Count > 0;
    }

    public void Done()
    {
        GetComponent<KutuphaneManager>().Congrats();
    }
}