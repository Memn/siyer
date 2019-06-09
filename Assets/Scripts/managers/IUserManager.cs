using System.Collections.Generic;
using UnityEngine.Events;

public interface IUserManager
{
    void Init(UnityAction<UnityAction<bool>> shouldMerge, UnityAction<List<User>, UnityAction<User>> userSelector);

    /**
     * User tries login
     */
    void Login(UnityAction<bool> callback);
//    void SyncUser();
//    void ReportScore();
//    void UnlockAchievement();
}