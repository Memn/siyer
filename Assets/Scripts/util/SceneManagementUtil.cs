using System;
using managers;
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
        EbuTalib = 8,
        Hatice = 9,
        Omer = 10,
        Ebubekir = 11,
    };

    public static string SceneName
    {
        get { return SceneManager.GetActiveScene().name; }
    }

    public static Scenes ActiveScene
    {
        get { return (Scenes) SceneManager.GetActiveScene().buildIndex; }
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == (int) Scenes.AnaEkran)
        {
            AchievementsManager.Instance.CheckLocks();
        }
    }

    public static void Load(Scenes scene)
    {
        SceneManager.LoadScene((int) scene);
    }

    public static void Load(CommonResources.Building building)
    {
        Load(ToScene(building));
    }

    private static Scenes ToScene(CommonResources.Building building)
    {
        switch (building)
        {
            case CommonResources.Building.Kabe:          return Scenes.Kabe;
            case CommonResources.Building.Abdulmuttalib: return Scenes.Abdulmuttalib;
            case CommonResources.Building.HzMuhammed:    return Scenes.HzMuhammed;
            case CommonResources.Building.DarulErkam:    return Scenes.DarulErkam;
            case CommonResources.Building.Hamza:         return Scenes.Hamza;
            case CommonResources.Building.Omer:          return Scenes.Omer;
            case CommonResources.Building.Ebubekir:      return Scenes.Ebubekir;
            case CommonResources.Building.Hatice:        return Scenes.Hatice;
            case CommonResources.Building.EbuTalib:      return Scenes.EbuTalib;
            default:
                throw new ArgumentOutOfRangeException("building", building, null);
        }
    }

    public static void Quit()
    {
        Application.Quit();
    }
}