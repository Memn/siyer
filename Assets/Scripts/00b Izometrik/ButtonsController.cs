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
        
//        if (!FacebookManager.Instance.IsLoggedIn)
//        {
//            Screen.orientation = ScreenOrientation.Portrait;
//            FacebookManager.Instance.LogIn();
//        }
//        else
//        {
//            FacebookManager.Instance.LogOut();
//            FBButtonImage(false);
//        }
    }

//    public void LoginCallback(ILoginResult result)
//    {
//        Screen.orientation = ScreenOrientation.Landscape;
//        if (string.IsNullOrEmpty(result.Error) || result.Cancelled)
//        {
//            if (!FacebookManager.Instance.IsLoggedIn) return;
//            // seems successfull
//            UserManager.Instance.LoggedIn();
//            FBButtonImage(true);
//        }
//        else
//        {
//            Debug.LogError("Login Error");
//            Debug.LogError(result.Error);
//        }
//    }

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