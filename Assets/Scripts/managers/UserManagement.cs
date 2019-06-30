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
        public User User;
        private UserDb _userDb;

        public static UserManagement Instance
        {
            get
            {
                if (_this)
                    return _this;
                else
                {
                    var obj = new GameObject("UserManagement");
                    obj.AddComponent<ScoreManager>();
                    obj.AddComponent<AchievementsManager>();
                    obj.AddComponent<ProgressManager>();
                    return _this = obj.AddComponent<UserManagement>();
                }
            }
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
                if (loginSuccessful)
                {
                    var localUserId = Social.localUser.id;
                    var localUserUserName = Social.localUser.userName;
                    if (_userDb.exists(localUserId))
                        User = _userDb.getDataById(Social.localUser.id);
                    else
                    {
                        shouldMerge(should =>
                        {
                            if (should)
                                User = _userDb.merge(localUserId, localUserUserName);
                            else
                                User = _userDb.CreateUser(new User(localUserId, localUserUserName));
                        });
                    }
                }
                else
                {
                    var users = _userDb.currentUsers();
                    if (users.Count == 0)
                        User = _userDb.CreateUser(new User());
                    else
                        selectUser(users, selection => User = selection);
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

        public void Save()
        {
            _userDb.update(User);
        }

    }
}