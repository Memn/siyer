using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UserSelector : MonoBehaviour
{
    public GameObject UserPrefab;
    public GameObject Panel;
    public GameObject Container;

    public void Open(List<User> users, UnityAction<string> callback)
    {
        Util.ClearChildren(Container.transform);
        Util.Load(Container, UserPrefab, users, (entry, member) =>
        {
            var usernameField = entry.GetComponentInChildren<Text>();
            usernameField.text = member.Username;
            entry.GetComponent<Button>().onClick.AddListener(() => callback(member.Id));
        });
        Panel.SetActive(true);
    }

    public bool IsOpen
    {
        get { return Panel.activeSelf; }
    }

    public void Close()
    {
        Panel.SetActive(false);
    }
}