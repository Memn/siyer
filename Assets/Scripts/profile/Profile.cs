using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;


public class Profile : MonoBehaviour
{

    public Image userImage;
    public Text userName;
    public Text score;
    public Text achievements;

    void Awake()
    {
        if (FB.IsLoggedIn)
        {
            FB.API("/me?fields=name", HttpMethod.GET, DisplayUsername);
            FB.API("/me/picture?type=square&width=128&height=128", HttpMethod.GET, DisplayProfilePic);
        }

    }

    void DisplayUsername(IResult result)
    {
        if (string.IsNullOrEmpty(result.Error))
        {
            userName.text = result.ResultDictionary["name"].ToString();
        }
        else
        {
            Debug.LogError("Cannot load user name");
            Debug.LogError(result.Error);
        }

    }
    void DisplayProfilePic(IGraphResult result)
    {
        if (string.IsNullOrEmpty(result.Error))
        {
            userImage.sprite = Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2());
        }
        else
        {
            Debug.LogError("Profile Picture cannot be loaded.");
            Debug.LogError(result.Error);
        }
    }

}
