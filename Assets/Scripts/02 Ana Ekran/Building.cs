using System;
using UnityEngine;

[Serializable]
public class Building : MonoBehaviour
{
    private SpriteRenderer _sprite;

    public SceneManagementUtil.Scenes Scene;
    public bool Selected;
    public string BuildingName;

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    public void GoScene()
    {
        SceneManagementUtil.Load(Scene);
    }

    public void Unselect()
    {
        _sprite.color = Color.white;
        Selected = false;
    }

    public void Select()
    {
        _sprite.color = Color.yellow;
        Selected = true;
    }

    public void Selection()
    {
        _sprite.color = Color.white;
        foreach (Transform child in transform)
        {
            child.GetComponent<SpriteRenderer>().color = Color.white;
        }

        Selected = true;
    }

    public void Darken()
    {
        _sprite.color = Color.gray;
        foreach (Transform child in transform)
        {
            child.GetComponent<SpriteRenderer>().color = Color.gray;
        }

        Selected = false;
    }

    public void Enlight()
    {
        _sprite.color = Color.white;
        foreach (Transform child in transform)
        {
            child.GetComponent<SpriteRenderer>().color = Color.white;
        }

        Selected = false;
    }
}