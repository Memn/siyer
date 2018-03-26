using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagementUtil : MonoBehaviour
{
    // build numbers
    public enum Scenes
    {
        Izometrik = 1,
        Profile = 2,
        AnaEkran = 3,
        Talimhane = 4,
        SoruCevap = 5,
        FilVakasi = 6,
        Labirent = 7,
        Kutuphane = 8
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