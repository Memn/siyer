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
}