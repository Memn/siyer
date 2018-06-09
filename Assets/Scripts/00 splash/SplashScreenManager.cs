using JetBrains.Annotations;
using UnityEngine;

public class SplashScreenManager : MonoBehaviour
{
    public float LoadMainMenuAfter = 3;


    private void Awake()
    {
        UserManager.Instance.Init();
    }

    [UsedImplicitly]
    public void LoadMainMenu()
    {
        SceneManagementUtil.Load(SceneManagementUtil.Scenes.Izometrik);
    }
}