using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagementUtil : MonoBehaviour
{
    // build numbers
    public enum Scenes
    {
        Izometrik = 1,
        AnaEkran = 2,
        Talimhane = 3,
        SoruCevap = 4,
        FilVakasi = 5,
        Labirent = 6,
        Kutuphane = 7
    };

    public static void Load(Scenes scene)
    {
        SceneManager.LoadScene((int) scene);
    }

    public static void Quit()
    {
        Application.Quit();
    }
}