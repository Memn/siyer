using System;
using System.Collections.Generic;
using UnityEngine;

public class CommonResources : MonoBehaviour
{
    // Binalar
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
    private static string Alim = SiyerResources.achievement_alim;
    private static string Okcubasi = SiyerResources.achievement_okuba;
    private static string Ustad = SiyerResources.achievement_stad;


    private static string MEKKE_MUHAFIZLARI = SiyerResources.achievement_mekke_muhafzlar;
    private static string ILIM_AVCILARI = SiyerResources.achievement_ilim_avclar;
    private static string CENGAVERLER = SiyerResources.achievement_cengaverler;
    private static string KAHRAMANLAR = SiyerResources.achievement_kahramanlar;
    private static string PEYGAMBER_DOSTLARI = SiyerResources.achievement_peygamber_dostlar;


    private static string Kasif = SiyerResources.achievement_kaif;
    private static string Kasif_2 = SiyerResources.achievement_kaif_2;
    private static string Kasif_3 = SiyerResources.achievement_kaif_3;
    private static string Kasif_4 = SiyerResources.achievement_kaif_4;
    private static string Kasif_5 = SiyerResources.achievement_kaif_5;

    private static string Talebe = SiyerResources.achievement_talebe;
    private static string Talebe_2 = SiyerResources.achievement_talebe_2;
    private static string Talebe_3 = SiyerResources.achievement_talebe_3;
    private static string Talebe_4 = SiyerResources.achievement_talebe_4;


    private static string Kemankes = SiyerResources.achievement_kemanke;
    private static string Kemankes_2 = SiyerResources.achievement_kemankes_2;
    private static string Kemankes_3 = SiyerResources.achievement_kemankes_3;

    private static string Muallim = SiyerResources.achievement_muallim;
    private static string Muallim_2 = SiyerResources.achievement_muallim_2;


    private static string Muhacir = SiyerResources.achievement_muhacir;


    public enum Building
    {
        Kabe = 3,
        Abdulmuttalib = 4,
        HzMuhammed = 5,
        DarulErkam = 6,
        Hamza = 7,
        EbuTalib = 8,
        Hatice = 9,
        Omer = 10,
        Ebubekir = 11,
        Muhacir
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
            Title = "Hikayeyi İzle, Abdulmuttalib'in Evi Açılsın!",
            Reward = Abdulmuttalib
        },
        new Duty
        {
            Building = Building.Abdulmuttalib,
            Requirement = 10,
            Title = "Abdulmuttalib'in Develerini Bul!",
            Reward = Kasif
        },
    };

    private static readonly List<Duty> Second = new List<Duty>
    {
        new Duty
        {
            Building = Building.HzMuhammed,
            Requirement = 0,
            Title = "Hikayeyi İzle, Darul Erkam Açılsın!",
            Reward = DarulErkam
        },
        new Duty
        {
            Building = Building.Abdulmuttalib,
            Requirement = 10,
            Title = "Abdulmuttalib'in Develerini Bul!",
            Reward = Kasif_2
        },
        new Duty
        {
            Building = Building.DarulErkam,
            Requirement = 0,
            Title = "Seviyeye ait kelimeleri tek seferde bitir",
            Reward = Talebe
        },
    };

    private static readonly List<Duty> Third = new List<Duty>
    {
        new Duty
        {
            Building = Building.Hamza,
            Requirement = 0,
            Title = "Hikayeyi İzle, Ebu Talib'in Evi Açılsın!",
            Reward = EbuTalib
        },
        new Duty
        {
            Building = Building.Abdulmuttalib,
            Requirement = 0,
            Title = "Abdulmuttalib'in Develerini Bul!",
            Reward = Kasif_3
        },
        new Duty
        {
            Building = Building.DarulErkam,
            Requirement = 0,
            Title = "Seviyeye ait kelimeleri tek seferde bitir",
            Reward = Talebe_2
        },
        new Duty
        {
            Building = Building.EbuTalib,
            Requirement = 6,
            Title = "10 Ok atisindan 6 tanesinde hedefi vur.",
            Reward = Kemankes
        },
    };

    private static readonly List<Duty> Fourth = new List<Duty>
    {
        new Duty
        {
            Building = Building.Hatice,
            Requirement = 0,
            Title = "Hikayeyi İzle, Hz. Ömer'in Evi Açılsın!",
            Reward = Omer
        },
        new Duty
        {
            Building = Building.Abdulmuttalib,
            Requirement = 10,
            Title = "Abdulmuttalib'in Develerini Bul!",
            Reward = Kasif_4
        },
        new Duty
        {
            Building = Building.DarulErkam,
            Requirement = 0,
            Title = "Seviyeye ait kelimeleri tek seferde bitir",
            Reward = Talebe_3
        },
        new Duty
        {
            Building = Building.EbuTalib,
            Requirement = 6,
            Title = "10 Ok atisindan 6 tanesinde hedefi vur.",
            Reward = Kemankes_2
        },
        new Duty
        {
            Building = Building.Omer,
            Requirement = 6,
            Title = "Seviyeye ait sorulardan en az 6 tanesini bil.",
            Reward = Muallim
        }
    };

    private static readonly List<Duty> Fifth = new List<Duty>
    {
        new Duty
        {
            Building = Building.Ebubekir,
            Requirement = 0,
            Title = "Hikayeyi İzle, Oyun Bitiyor!",
            Reward = Muhacir
        },
        new Duty
        {
            Building = Building.Abdulmuttalib,
            Requirement = 10,
            Title = "Abdulmuttalib'in Develerini Bul!",
            Reward = Kasif_5
        },
        new Duty
        {
            Building = Building.DarulErkam,
            Requirement = 0,
            Title = "Seviyeye ait kelimeleri tek seferde bitir",
            Reward = Talebe_4
        },
        new Duty
        {
            Building = Building.EbuTalib,
            Requirement = 8,
            Title = "10 Ok atisindan 8 tanesinde hedefi vur.",
            Reward = Kemankes_3
        },
        new Duty
        {
            Building = Building.Omer,
            Requirement = 8,
            Title = "Seviyeye ait sorulardan en az 8 tanesini bil.",
            Reward = Muallim_2
        }
    };

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
            case Building.Muhacir:       return Muhacir;
            default:
                throw new ArgumentOutOfRangeException("building", building, null);
        }
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
    public static string LevelsText(int level)
    {
        switch (level)
        {
            case 1:
                return "MEKKE MUHAFIZLARI";
            case 2:
                return "ILIM AVCILARI";
            case 3:
                return "CENGAVERLER";
            case 4:
                return "KAHRAMANLAR";
            case 5:
                return "PEYGAMBER DOSTLARI";
            default:
                throw new ArgumentOutOfRangeException(level + "level is not known");
        }
    }

    public static string Stories(int level)
    {
        switch (level)
        {
            case 2:
                return HzMuhammed;
            case 3:
                return Hamza;
            case 4:
                return Hatice;
            case 5:
                return Ebubekir;
            default:
                throw new ArgumentOutOfRangeException(level + "level is not known");
        }
    }

    public static string Extras(Building building)
    {
        // ReSharper disable once SwitchStatementMissingSomeCases
        switch (building)
        {
            case Building.Abdulmuttalib: return Muhafiz;
            case Building.DarulErkam:    return Alim;
            case Building.EbuTalib:      return Okcubasi;
            case Building.Omer:          return Ustad;
            default:
                throw new ArgumentOutOfRangeException("building", building, null);
        }
    }
}