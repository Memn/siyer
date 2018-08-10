using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsController : MonoBehaviour
{
    public static ButtonsController Instance { get; private set; }
    public Button FbButton;
    public Button ProfileButton;
    public ProfileHelper ProfileHelper;

    private void Awake()
    {
        Instance = this;
    }


    [UsedImplicitly]
    public void Login()
    {
    }

    public void ProfileLoaded()
    {
        ProfileButton.interactable = true;
        ProfileHelper.LoadUser();
    }

    private void FBButtonImage(bool loggedIn)
    {
        FbButton.image.color = loggedIn ? Color.red : Color.white;
    }
}