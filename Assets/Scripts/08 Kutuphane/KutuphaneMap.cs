﻿using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class KutuphaneMap : MonoBehaviour
{
    public GameObject PuzzleObject;
    public Transform PuzzleParentTransform;
    private int _width = 7;
    private int _height = 5;
    private const float ScalingFactor = 1f;
    private const float XSpacing = 1.00f;
    private const float YSpacing = 0.9f;

    private List<string> _words;
    private Puzzle _currentPuzzle;

    public void StartPuzzle()
    {
        Puzzle2Map(_currentPuzzle);
    }

    public void LoadPuzzle(string word)
    {
        _words = word.ToUpper(new CultureInfo("tr-TR", false)).Split(' ').ToList();
        foreach (Transform child in PuzzleParentTransform)
        {
            Destroy(child.gameObject);
        }

        var sentence = Regex.Replace(word.ToUpper(new CultureInfo("tr-TR", false)), @"\s+", "");
        _currentPuzzle = PuzzleMaker.MakePuzzle(sentence);
    }

    private void Puzzle2Map(Puzzle makePuzzle)
    {
        var startY = -1 * makePuzzle.height / 2;
        var startX = -1 * makePuzzle.width / 2;
        var start = new Vector2(startX, startY);
        for (var x = 0; x < makePuzzle.width; x++)
        {
            for (var y = 0; y < makePuzzle.height; y++)
            {
                var fy = start.y + y * YSpacing;
                var fx = start.x + x * XSpacing;

                if (y % 2 == 0)
                {
                    fx += 0.3f;
                }

                var pos = new Vector2(fx, fy);
                var go = Instantiate(PuzzleObject, Vector3.zero, Quaternion.identity);
                go.GetComponent<PuzzleObject>().SetCharacter(makePuzzle.puzzleData[x, y]);
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