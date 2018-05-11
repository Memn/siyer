using UnityEngine;

public class SplashScreenManager : MonoBehaviour
{
    public float LoadMainMenuAfter = 3;

    // TODO: Remove from IzometrikHarita
//    private void Awake()
//    {
//        FacebookManager.Instance.InitFB();
//        UserManager.Instance.Init();
//    }

    private void Start()
    {
        
        if (LoadMainMenuAfter > 0)
            Invoke("LoadMainMenu", LoadMainMenuAfter);
    }

    private void LoadMainMenu()
    {
        SceneManagementUtil.Load(SceneManagementUtil.Scenes.Izometrik);
    }
}