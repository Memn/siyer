using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MenuTabsHandler : MonoBehaviour
{
    private Button[] _tabs;

    public GameObject Helper;


    private void Awake()
    {
        _tabs = GetComponentsInChildren<Button>();
    }

    public void Init()
    {
        SelectTab(_tabs[0]);
        var unused = _tabs.All(tab =>
        {
            tab.onClick.AddListener(() => SelectTab(tab));
            return true;
        });
    }

    private void SelectTab(Selectable tab)
    {
        var unused = _tabs.All(Unselect);
        Select(tab);
    }

    // ReSharper disable once MemberCanBeMadeStatic.Local
    private void Select(Selectable tab)
    {
        tab.image.color = Color.white;
        Helper.GetComponent<LoadableHelper>().Load(tab);
    }

    // ReSharper disable once MemberCanBeMadeStatic.Local
    private bool Unselect(Selectable tab)
    {
        tab.image.color = Color.gray;
        return true;
    }
}