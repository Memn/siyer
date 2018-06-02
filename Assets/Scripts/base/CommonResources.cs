using System;
using System.Collections.Generic;
using UnityEngine;

public class CommonResources : MonoBehaviour
{
#if UNITY_EDITOR
    // Binalar
    private static string Kabe = "Achievement01";
    private static string Abdulmuttalib = "Achievement01";
    private static string HzMuhammed = "Achievement03";
    private static string DarulErkam = "Achievement03";
    private static string Hamza = "Achievement01";
    private static string Omer = "Achievement01";
    private static string Ebubekir = "Achievement01";
    private static string Hatice = "Achievement01";
    private static string EbuTalib = "Achievement01";
    private static string Muhafiz = "Achievement02";
    private static string Talebe = "Achievement02";
    private static string Okcubasi = "Achievement02";
    private static string Muallim = "Achievement02";

    private static string MEKKE_MUHAFIZLARI = "Achievement01";
    private static string ILIM_AVCILARI = "Achievement01";
    private static string CENGAVERLER = "Achievement01";
    private static string KAHRAMANLAR = "Achievement01";
    private static string PEYGAMBER_DOSTLARI = "Achievement01";
#elif UNITY_ANDROID
    private static string Kabe = SiyerResources.achievement_kabe;
    private static string Abdulmuttalib = SiyerResources.achievement_abdulmuttalibin_evi;
    private static string HzMuhammed = SiyerResources.achievement_peygamberimizin_evi;
    private static string DarulErkam = SiyerResources.achievement_darul_erkam;
    private static string Hamza = SiyerResources.achievement_hz_hamzann_evi;
    private static string Omer = SiyerResources.achievement_hz_merin_evi;
    private static string Ebubekir = SiyerResources.achievement_hz_ebubekirin_evi;
    private static string Hatice = SiyerResources.achievement_hz_haticenin_evi;
    private static string EbuTalib = SiyerResources.achievement_ebu_talibin_evi;
    private static string Muhafiz = SiyerResources.achievement_muhafz;
    private static string Talebe = SiyerResources.achievement_talebe;
    private static string Okcubasi = SiyerResources.achievement_okuba;
    private static string Muallim = SiyerResources.achievement_muallim;
    
    private static string MEKKE_MUHAFIZLARI = SiyerResources.achievement_mekke_muhafzlar;
    private static string ILIM_AVCILARI = SiyerResources.achievement_ilim_avclar;
    private static string CENGAVERLER = SiyerResources.achievement_cengaverler;
    private static string KAHRAMANLAR = SiyerResources.achievement_kahramanlar;
    private static string PEYGAMBER_DOSTLARI = SiyerResources.achievement_peygamber_dostlar;
#endif
    // Levels

    public static object ExtraRewards
    {
        get { return _extraRewards; }
        set { _extraRewards = value; }
    }


    public enum Building
    {
        Kabe = 3,
        Abdulmuttalib = 4,
        HzMuhammed = 5,
        DarulErkam = 6,
        Hamza = 7,
        Omer = 8,
        Ebubekir = 9,
        Hatice = 10,
        EbuTalib = 11
    }

    public static readonly Dictionary<Building, Description> Descriptions = new Dictionary<Building, Description>
    {
        {
            Building.Kabe,
            new Description
            {
                Title = "Kâbe",
                Info = "Allah katında çok kıymetli olan Kâbe, " +
                       "Hz. İbrahim tarafından inşa edildiği zamandan itibaren insanlar " +
                       "tarafından da daima kutsal kabul edilmiştir. Müslümanların kıblesidir."
            }
        },
        {
            Building.Abdulmuttalib,
            new Description
            {
                Title = "Abdulmuttalib'in Evi",
                Info = "Peygamberimizin dedesi olan Abdulmuttalib, Mekke'nin Reisidir. " +
                       "Peygamberimiz annesinin vefatından sonra iki sene kadar " +
                       "dedesi Abdulmuttalib'in himayesinde kalmıştır."
            }
        },
        {
            Building.HzMuhammed,
            new Description
            {
                Title = "Peygamberimizin Evi",
                Info = "571 yılında Mekke’de doğdu. O’nun ahlakı, Kur'an ahlakıdır. " +
                       "Hz. Muhammed (s.a.v.) son peygamberdir."
            }
        },
        {
            Building.DarulErkam,
            new Description
            {
                Title = "Darul Erkam",
                Info = "İslamiyet’in ilk yıllarında henüz gizli davet yapılırken, Hz. Erkam evini bağışlamış; " +
                       "Darul Erkam İslamiyet’in ilk medresesi olmuştur."
            }
        },
        {
            Building.Hamza,
            new Description
            {
                Title = "Hz. Hamza’nın Evi",
                Info = "Kureyşin en şereflilerinden olan Hz. Hamza, iyi bir avcı, keskin bir nişancıdır. " +
                       "Peygamber Efendimizin amcasıdır."
            }
        },
        {
            Building.Omer,
            new Description
            {
                Title = "Hz. Ömer’in Evi",
                Info = "İslam'ın ikinci halifesi olan Hz. Ömer, nüfuzuyla güç ve kuvvetiyle meşhurdu. " +
                       "Onun iman etmesi Müslümanlara büyük bir kuvvet kazandırdı. " +
                       "Hatta Müslüman olduğu gün bütün Müslümanlar Kâbe’ye giderek ilk defa açıktan namaz kıldılar."
            }
        },
        {
            Building.Ebubekir,
            new Description
            {
                Title = "Hz. Ebu Bekir'in Evi",
                Info = "Hz. Ebu Bekir, Peygamber Efendimizin en yakın dostlarındandır. " +
                       "Hatta hicrette de yoldaşı olmuştur. Bir an bile tereddüt etmeden Müslüman olan " +
                       "Hz. Ebu Bekir aynı zamanda İslamiyet’in ilk halifesidir."
            }
        },
        {
            Building.Hatice,
            new Description
            {
                Title = "Hz. Hatice’nin Evi",
                Info = "Yeryüzünde İslam'a ilk inanan insan olan Hz. Hatice, Efendimizin de ilk eşidir. " +
                       "Efendimizin Peygamberliğini ilk tasdik eden ve her türlü sıkıntıda daima yanında olan " +
                       "Hz. Hatice’nin vefatı hüzün yılı olarak adlandırılmıştır."
            }
        },
        {
            Building.EbuTalib,
            new Description
            {
                Title = "Ebu Talib'in Evi",
                Info = "İlk Müslüman çocuk olan Hz. Ali’nin babası, Efendimizin amcasıdır. " +
                       "Peygamber Efendimiz sekiz yaşındayken dedesi vefat edince, amcası " +
                       "Ebu Talib'in himayesinde kalmıştır."
            }
        }
    };

    public static List<Duty> DutyOf(int level)
    {
        switch (level)
        {
            case 1:
                return First;
            case 2:
                return Second;
            case 3:
                return Third;
            case 4:
                return Fourth;
            case 5:
                return Fifth;
            default:
                throw new ArgumentOutOfRangeException(level + "level is not known");
        }
    }

    private static readonly List<Duty> First = new List<Duty>
    {
        new Duty
        {
            Building = Building.Kabe,
            Requirement = 0,
            Title = "Fil Vakasi animasyonlarini bitir.",
            Reward = Abdulmuttalib
        },
        new Duty
        {
            Building = Building.Abdulmuttalib,
            Requirement = 10,
            Title = "Labirent icerisindeki develeri verilen surede bul.",
            Reward = Muhafiz
        },
    };

    private static readonly List<Duty> Second = new List<Duty>
    {
        new Duty {Building = Building.HzMuhammed, Requirement = 0, Title = "Animasyonlari bitir.", Reward = DarulErkam},
        new Duty
        {
            Building = Building.Abdulmuttalib,
            Requirement = 10,
            Title = "Labirent icerisindeki develeri verilen surede bul."
        },
        new Duty {Building = Building.DarulErkam, Requirement = 0, Title = "Seviyeye ait kelimeleri tek seferde bitir"},
    };

    private static readonly List<Duty> Third = new List<Duty>
    {
        new Duty {Building = Building.Hamza, Requirement = 0, Title = "Animasyonlari bitir."},
        new Duty
        {
            Building = Building.Abdulmuttalib,
            Requirement = 10,
            Title = "Labirent icerisindeki develeri verilen surede bul."
        },
        new Duty {Building = Building.DarulErkam, Requirement = 0, Title = "Seviyeye ait kelimeleri tek seferde bitir"},
        new Duty {Building = Building.Omer, Requirement = 6, Title = "10 Ok atisindan 6 tanesinde hedefi vur."},
    };

    private static readonly List<Duty> Fourth = new List<Duty>
    {
        new Duty {Building = Building.Ebubekir, Requirement = 0, Title = "Animasyonlari bitir."},
        new Duty
        {
            Building = Building.Abdulmuttalib,
            Requirement = 10,
            Title = "Labirent icerisindeki develeri verilen surede bul."
        },
        new Duty {Building = Building.DarulErkam, Requirement = 0, Title = "Seviyeye ait kelimeleri tek seferde bitir"},
        new Duty {Building = Building.Omer, Requirement = 6, Title = "10 Ok atisindan 6 tanesinde hedefi vur."},
        new Duty {Building = Building.Hatice, Requirement = 6, Title = "Seviyeye ait sorulardan en az 6 tanesini bil."}
    };

    private static readonly List<Duty> Fifth = new List<Duty>
    {
        new Duty {Building = Building.EbuTalib, Requirement = 0, Title = "Animasyonlari bitir."},
        new Duty
        {
            Building = Building.Abdulmuttalib,
            Requirement = 10,
            Title = "Labirent icerisindeki develeri verilen surede bul."
        },
        new Duty {Building = Building.DarulErkam, Requirement = 0, Title = "Seviyeye ait kelimeleri tek seferde bitir"},
        new Duty {Building = Building.Omer, Requirement = 8, Title = "10 Ok atisindan 8 tanesinde hedefi vur."},
        new Duty {Building = Building.Hatice, Requirement = 8, Title = "Seviyeye ait sorulardan en az 8 tanesini bil."}
    };

    private static object _extraRewards;

    public class Duty
    {
        public string Title;
        public int Requirement;
        public Building Building;
        public string Reward;
    }

    public class Description
    {
        public static readonly Description None = new Description {Title = "", Info = ""};
        public string Title;
        public string Info;
    }

    public static string IdOf(Building building)
    {
        switch (building)
        {
            case Building.Kabe:          return Kabe;
            case Building.Abdulmuttalib: return Abdulmuttalib;
            case Building.HzMuhammed:    return HzMuhammed;
            case Building.DarulErkam:    return DarulErkam;
            case Building.Hamza:         return Hamza;
            case Building.Omer:          return Omer;
            case Building.Ebubekir:      return Ebubekir;
            case Building.Hatice:        return Hatice;
            case Building.EbuTalib:      return EbuTalib;
            default:
                throw new ArgumentOutOfRangeException("building", building, null);
        }
    }


    public static string IdOf(Building building, int currentLevel)
    {
        switch (building)
        {
            case Building.Abdulmuttalib:
                return Muhafiz;
            case Building.DarulErkam:
                return Talebe;
            case Building.Omer:
                return Okcubasi;
            case Building.Hatice:
                return Muallim;
        }

        return "";
    }

    public static string Levels(int level)
    {
        switch (level)
        {
            case 1:
                return MEKKE_MUHAFIZLARI;
            case 2:
                return ILIM_AVCILARI;
            case 3:
                return CENGAVERLER;
            case 4:
                return KAHRAMANLAR;
            case 5:
                return PEYGAMBER_DOSTLARI;
            default:
                throw new ArgumentOutOfRangeException(level + "level is not known");
        }
    }

    public static Building Stories(int level)
    {
        switch (level)
        {
            case 2:
                return Building.HzMuhammed;
            case 3:
                return Building.Hamza;
            case 4:
                return Building.Ebubekir;
            case 5:
                return Building.EbuTalib;
            default:
                throw new ArgumentOutOfRangeException(level + "level is not known");
        }
    }
}