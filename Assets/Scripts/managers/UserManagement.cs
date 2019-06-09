#if UNITY_ANDROID && !UNITY_EDITOR
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
#endif
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace managers
{
    public class UserManagement : MonoBehaviour, IUserManager
    {
        private static UserManagement _this;
        private User _user;
        private UserDb _userDb;

        public static UserManagement Instance
        {
            get { return _this ? _this : (_this = new GameObject("UserManagement").AddComponent<UserManagement>()); }
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void Init(UnityAction<UnityAction<bool>> shouldMerge,
            UnityAction<List<User>, UnityAction<User>> selectUser)
        {
            _userDb = new UserDb();
            Login(loginSuccessful =>
            {
                if (false)
                {
                    var localUserId = Social.localUser.id;
                    var localUserUserName = Social.localUser.userName;
                    if (_userDb.exists(localUserId))
                        _user = _userDb.getDataById(Social.localUser.id);
                    else
                    {
                        shouldMerge(should =>
                        {
                            if (should)
                                _user = _userDb.merge(localUserId, localUserUserName);
                            else
                                _user = _userDb.CreateUser(new User(localUserId, localUserUserName));
                        });
                    }
                }
                else
                {
                    var users = _userDb.currentUsers();
                    if (users.Count == 0)
                        _user = _userDb.CreateUser(new User());
                    else
                        selectUser(users, selection => _user = selection);
                }
            });
        }

        public void Login(UnityAction<bool> callback)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
        LogUtil.Log("Play Games Activation started");
        var config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.DebugLogEnabled = LogUtil.Debugging;
#endif

            Social.localUser.Authenticate(result => callback(result));
        }
    }
}