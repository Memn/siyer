using Facebook.Unity;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsController : MonoBehaviour
{
    public static ButtonsController Instance { get; private set; }
    public Button FbButton;
    public Button ProfileButton;

    private void Awake()
    {
        Instance = this;
        UserManager.Instance.Init();
    }

    private void Start()
    {
        var isLoggedIn = FacebookManager.Instance.IsLoggedIn;
        FbButton.interactable = !isLoggedIn;
        ProfileButton.interactable = false;
    }

    [UsedImplicitly]
    public void Login()
    {
        if (!FacebookManager.Instance.IsLoggedIn)
        {
            Screen.orientation = ScreenOrientation.Portrait;
            FacebookManager.Instance.LogIn();
        }
        else
        {
            FacebookManager.Instance.LogOut();
            FbButton.image.color = Color.white;
        }
    }

    public void LoginCallback(ILoginResult result)
    {
        Screen.orientation = ScreenOrientation.Landscape;
        if (string.IsNullOrEmpty(result.Error) || result.Cancelled)
        {
            if (!FacebookManager.Instance.IsLoggedIn) return;
            // seems successfull
            UserManager.Instance.LoggedIn();
            FbButton.image.color = Color.red;
        }
        else
        {
            Debug.LogError("Login Error");
            Debug.LogError(result.Error);
        }
    }

    public void ProfileLoaded()
    {
        ProfileButton.interactable = true;
    }
}