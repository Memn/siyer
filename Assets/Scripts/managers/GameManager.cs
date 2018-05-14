using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void Load(SceneManagementUtil.Scenes scene)
    {
        SceneManagementUtil.Load(scene);
    }

    public void QuitRequest()
    {
        Application.Quit();
    }

    public void ShowAchievements()
    {
        if (UserManager.Instance.Connect2GoogleServices())
        {
            Debug.Log("Show Achievements...");
            UserManager.Instance.ToAchievements();
        }
        else
        {
            Debug.Log("Cannot connect 2 Google Services..");
        }
    }
}