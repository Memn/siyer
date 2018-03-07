﻿using System.Collections.Generic;
using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    public GameObject AnswerObject;
    public Transform AnswerParentTransform;
    public KutuphaneMap Map;

    private Stack<PuzzleObject> _selectedPuzzleObjects;
    private string _answer;

    private void Start()
    {
        _answer = "";
        _selectedPuzzleObjects = new Stack<PuzzleObject>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            ControlState();
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        var pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        var hitInfo = Physics2D.Raycast(_camera.ScreenToWorldPoint(pos), Vector2.zero);
        // RaycastHit2D can be either true or null, but has an implicit conversion to bool, so we can use it like this
        if (hitInfo)
        {
            HandleTouchOn(hitInfo.transform.gameObject);
            // Here you can check hitInfo to see which collider has been hit, and act appropriately.
        }
    }

    private void ControlState()
    {
        if (Map.CheckAnswer(_answer))
        {
            Debug.Log("Success");
            while (_selectedPuzzleObjects.Count > 0)
            {
                _selectedPuzzleObjects.Pop().Correctify();
                Pop();
            }

            if (!Map.AnyWordsLeft())
            {
                Map.Done();
            }
        }
        else
        {
            Debug.Log("Fail");
            while (_selectedPuzzleObjects.Count > 0)
            {
                _selectedPuzzleObjects.Pop().Unselect();
                Pop();
            }
        }

        _answer = "";
        foreach (Transform child in AnswerParentTransform)
        {
            Destroy(child.gameObject);
        }
    }

    private void HandleTouchOn(GameObject transformGameObject)
    {
        var puzzleComponent = transformGameObject.GetComponent<PuzzleObject>();
        if (!puzzleComponent) return;
        if (_selectedPuzzleObjects.Count == 0)
        {
            Push(puzzleComponent);
        }
        else if (puzzleComponent == _selectedPuzzleObjects.Peek()) return; // holding last
        else
        {
            if (!AreNeighbours(puzzleComponent, _selectedPuzzleObjects.Peek())) return;
            if (!puzzleComponent.isSelected())
            {
                Push(puzzleComponent);
            }
            else
            {
                // is it before last selected item.
                var last = _selectedPuzzleObjects.Pop();
                if (_selectedPuzzleObjects.Peek() == puzzleComponent)
                {
                    last.Unselect();
                    Pop();
                }
                else
                {
                    _selectedPuzzleObjects.Push(last);
                }
            }
        }
    }

    private void Pop()
    {
        Destroy(AnswerParentTransform.GetChild(AnswerParentTransform.childCount - 1).gameObject);
        AnswerParentTransform.position += Vector3.right * 0.4f;
        _answer = _answer.Remove(_answer.Length - 1);
    }

    private bool AreNeighbours(PuzzleObject puzzleComponent, PuzzleObject last)
    {
        return puzzleComponent != last;
    }

    private void Push(PuzzleObject puzzleObject)
    {
        puzzleObject.Select();
        _selectedPuzzleObjects.Push(puzzleObject);
        var c = puzzleObject.Letter;
        _answer += c;

        var go = Instantiate(AnswerObject, Vector3.zero, Quaternion.identity);
        go.GetComponent<PuzzleObject>().SetCharacter(c);
        go.transform.parent = AnswerParentTransform;
        go.transform.localPosition = Vector2.right * 0.8f * _selectedPuzzleObjects.Count;
        go.transform.localScale = Vector3.one;
        AnswerParentTransform.position += Vector3.left * 0.4f;
    }
}