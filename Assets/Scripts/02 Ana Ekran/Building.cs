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
}