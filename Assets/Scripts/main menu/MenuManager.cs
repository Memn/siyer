using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Text loginLogoutButton;
    public Button profileButton; 
    void Awake()
    {
        FacebookManager.Instance.InitFB();
        DealWithMenu();
    }

    void DealWithMenu()
    {
        if (FacebookManager.Instance.IsLoggedIn)
        {
            loginLogoutButton.text = "Logout";
            profileButton.interactable = true;
        }
        else
        {
            loginLogoutButton.text = "Login";
            profileButton.interactable = false;
        }
    }

    public void FacebookLogInOut()
    {
        if (FacebookManager.Instance.IsLoggedIn)
        {
            FacebookManager.Instance.LogOut();
        }
        else
        {
            FacebookManager.Instance.LogIn();
        }
        DealWithMenu();
    }
}
