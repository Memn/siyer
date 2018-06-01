using System;
using System.Collections.Generic;
using UnityEngine;

public class CommonResources : MonoBehaviour
{
    public enum Resource
    {
        Kabe,
        Abdulmuttalib,
        HzMuhammed,
        DarulErkam,
        Hamza,
        Omer,
        Ebubekir,
        Hatice,
        EbuTalib
    }


    public static string IdOf(Resource resource)
    {
        switch (resource)
        {
            case Resource.Kabe:          return Kabe;
            case Resource.Abdulmuttalib: return Abdulmuttalib;
            case Resource.HzMuhammed:    return HzMuhammed;
            case Resource.DarulErkam:    return DarulErkam;
            case Resource.Hamza:         return Hamza;
            case Resource.Omer:          return Omer;
            case Resource.Ebubekir:      return Ebubekir;
            case Resource.Hatice:        return Hatice;
            case Resource.EbuTalib:      return EbuTalib;
            default:
                throw new ArgumentOutOfRangeException("resource", resource, null);
        }
    }


    public static List<string> Buildings = new List<string>
    {
        Kabe,
        Abdulmuttalib,
        HzMuhammed,
        DarulErkam,
        Hamza,
        Omer,
        Ebubekir,
        Hatice,
        EbuTalib,
    };

    static CommonResources()
    {

        Kabe = SiyerResources.achievement_kabe;
        Abdulmuttalib = SiyerResources.achievement_abdulmuttalibin_evi;
        HzMuhammed = SiyerResources.achievement_peygamberimizin_evi;
        DarulErkam = SiyerResources.achievement_darul_erkam;
        Hamza = SiyerResources.achievement_hz_hamzann_evi;
        Omer = SiyerResources.achievement_hz_merin_evi;
        Ebubekir = SiyerResources.achievement_hz_ebubekirin_evi;
        Hatice = SiyerResources.achievement_hz_haticenin_evi;
        EbuTalib = SiyerResources.achievement_ebu_talibin_evi;

        Muhafiz = SiyerResources.achievement_muhafz;
        Talebe = SiyerResources.achievement_talebe;
        Okcubasi = SiyerResources.achievement_okuba;
        Muallim = SiyerResources.achievement_muallim;
#if UNITY_EDITOR
        Kabe = "Achievement01";
        Abdulmuttalib = "Achievement02";
        HzMuhammed = "Achievement01";
        DarulErkam = "Achievement02";
        Hamza = "Achievement01";
        Omer = "Achievement01";
        Ebubekir = "Achievement01";
        Hatice = "Achievement01";
        EbuTalib = "Achievement01";

        Muhafiz = "Achievement03";
        Talebe = "Achievement02";
        Okcubasi = "Achievement02";
        Muallim = "Achievement02";
#endif
    }

    // Binalar
    public static string Kabe { get; set; }
    public static string Abdulmuttalib { get; set; }
    public static string HzMuhammed { get; set; }
    public static string DarulErkam { get; set; }
    public static string Hamza { get; set; }
    public static string Omer { get; set; }
    public static string Ebubekir { get; set; }
    public static string Hatice { get; set; }
    public static string EbuTalib { get; set; }

    // Rozetler
    public static string Muhafiz { get; set; }
    public static string Talebe { get; set; }
    public static string Okcubasi { get; set; }
    public static string Muallim { get; set; }


    public static string IdOf(Resource resource, int currentLevel)
    {
        switch (resource)
        {
            case Resource.Abdulmuttalib:
                return Muhafiz;
            case Resource.DarulErkam:
                return Talebe;
            case Resource.Omer:
                return Okcubasi;
            case Resource.Hatice:
                return Muallim;
        }

        return "";
    }
}