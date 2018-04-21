using System.Collections.Generic;
using System.IO;
using Facebook.MiniJSON;
using Facebook.Unity;
using UnityEngine;

public class UserManager : MonoBehaviour
{
    private static UserManager _instance;

    public static UserManager Instance
    {
        get
        {
            if (_instance != null)
                return _instance;
            var fbm = new GameObject("UserManager");
            _instance = fbm.AddComponent<UserManager>();
            return _instance;
        }
    }

    public User User;

    private string userFileName;

    private string _userFilePath
    {
        get { return Path.Combine(Application.persistentDataPath, userFileName); }
        set { userFileName = value; }
    }

    public void Init()
    {
    }

    private void Awake()
    {
        FacebookManager.Instance.InitFB();
        // only game object
        if (FacebookManager.Instance.IsLoggedIn)
        {
            LoggedIn();
        }
        else
        {
            _userFilePath = "guest.data";
            var fromFile = Util.LoadUserFromFile(_userFilePath);
            if (User.Default == fromFile)
            {
                User = new User(fromFile);
                Util.SaveUser(User, _userFilePath);
            }
            else
            {
                User = fromFile;
            }

            ButtonsController.Instance.ProfileLoaded();
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void BasicProfileCallback(IResult result)
    {
        if (string.IsNullOrEmpty(result.Error))
        {
            User.Name = result.ResultDictionary["name"].ToString();
            User.FacebookID = result.ResultDictionary["id"].ToString();
        }
        else
        {
            Debug.LogError("Cannot load user name");
            Debug.LogError(result.Error);
        }
        
        Util.SaveUser(User, _userFilePath);
    }

    private void ProfilePicCallback(IGraphResult result)
    {
        if (string.IsNullOrEmpty(result.Error))
        {
            User.ProfilePic = Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2());
        }
        else
        {
            Debug.LogError("Profile Picture cannot be loaded.");
            Debug.LogError(result.Error);
        }
        
        Util.SaveUser(User, _userFilePath);
    }

    private void FriendsCallback(IResult result)
    {
        var dictionary = (Dictionary<string, object>) Json.Deserialize(result.RawResult);
        var friendsList = (List<object>) dictionary["data"];
        foreach (Dictionary<string, object> friend in friendsList)
        {
            var friendName = friend["name"].ToString();
            var id = friend["id"].ToString();
            User.Friends.Add(id, friendName);
        }

        Util.SaveUser(User, _userFilePath);
    }

    public void LoggedIn()
    {
        FacebookManager.Instance.LoadProfile(result =>
        {
            _userFilePath = result.ResultDictionary["id"].ToString() + ".data";
            var fromFile = Util.LoadUserFromFile(_userFilePath);
            if (fromFile == User.Default)
            {
                User = new User(fromFile);
                FacebookManager.Instance.LoadProfile(BasicProfileCallback, ProfilePicCallback, FriendsCallback);
            }
            else
            {
                User = fromFile;
                FacebookManager.Instance.LoadProfile(null, null, FriendsCallback);
            }

            ButtonsController.Instance.ProfileLoaded();
        });
    }
}