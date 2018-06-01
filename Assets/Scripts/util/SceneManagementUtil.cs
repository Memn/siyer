using System;
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

    public static void Load(CommonResources.Resource resource)
    {
        Load(ToScene(resource));
    }

    private static Scenes ToScene(CommonResources.Resource resource)
    {
        switch (resource)
        {
            case CommonResources.Resource.Kabe:          return Scenes.Kabe;
            case CommonResources.Resource.Abdulmuttalib: return Scenes.Abdulmuttalib;
            case CommonResources.Resource.HzMuhammed:    return Scenes.HzMuhammed;
            case CommonResources.Resource.DarulErkam:    return Scenes.DarulErkam;
            case CommonResources.Resource.Hamza:         return Scenes.Hamza;
            case CommonResources.Resource.Omer:          return Scenes.Omer;
            case CommonResources.Resource.Ebubekir:      return Scenes.Ebubekir;
            case CommonResources.Resource.Hatice:        return Scenes.Hatice;
            case CommonResources.Resource.EbuTalib:      return Scenes.EbuTalib;
            default:
                throw new ArgumentOutOfRangeException("resource", resource, null);
        }
    }

    public static void Quit()
    {
        Application.Quit();
    }
}