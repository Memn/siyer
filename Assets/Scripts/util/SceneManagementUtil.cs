using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagementUtil : MonoBehaviour
{
    // build numbers
    public enum Scenes
    {
        // no route to splash back 
//        Splash = 0
        Izometrik = 1,
        AnaEkran = 2,
        Kabe = 3,
        Abdulmuttalib = 4,
        HzMuhammed = 5,
        DarulErkam = 6,
        Hamza = 7,
        Omer = 8,
        Ebubekir = 9,
        Hatice = 10,
        EbuTalib = 11
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