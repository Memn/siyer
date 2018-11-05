using System;
using System.Collections.Generic;
using UnityEngine;

public class CommonResources : MonoBehaviour
{
    // Binalar
    private const string Kabe = SiyerResources.achievement_kabe;
    private const string Abdulmuttalib = SiyerResources.achievement_abdulmuttalibin_evi;
    private const string HzMuhammed = SiyerResources.achievement_peygamberimizin_evi;
    private const string DarulErkam = SiyerResources.achievement_darul_erkam;
    private const string Hamza = SiyerResources.achievement_hz_hamzann_evi;
    private const string Omer = SiyerResources.achievement_hz_merin_evi;
    private const string Ebubekir = SiyerResources.achievement_hz_ebubekirin_evi;
    private const string Hatice = SiyerResources.achievement_hz_haticenin_evi;
    private const string EbuTalib = SiyerResources.achievement_ebu_talibin_evi;

    private const string Muhafiz = SiyerResources.achievement_muhafz;
    private const string Alim = SiyerResources.achievement_alim;
    private const string Okcubasi = SiyerResources.achievement_okuba;
    private const string Ustad = SiyerResources.achievement_stad;


    private const string MEKKE_MUHAFIZLARI = SiyerResources.achievement_mekke_muhafzlar;
    private const string ILIM_AVCILARI = SiyerResources.achievement_ilim_avclar;
    private const string CENGAVERLER = SiyerResources.achievement_cengaverler;
    private const string KAHRAMANLAR = SiyerResources.achievement_kahramanlar;
    private const string PEYGAMBER_DOSTLARI = SiyerResources.achievement_peygamber_dostlar;


    private const string Kasif = SiyerResources.achievement_kaif;
    private const string Kasif_2 = SiyerResources.achievement_kaif_2;
    private const string Kasif_3 = SiyerResources.achievement_kaif_3;
    private const string Kasif_4 = SiyerResources.achievement_kaif_4;
    private const string Kasif_5 = SiyerResources.achievement_kaif_5;

    private const string Talebe = SiyerResources.achievement_talebe;
    private const string Talebe_2 = SiyerResources.achievement_talebe_2;
    private const string Talebe_3 = SiyerResources.achievement_talebe_3;
    private const string Talebe_4 = SiyerResources.achievement_talebe_4;


    private const string Kemankes = SiyerResources.achievement_kemanke;
    private const string Kemankes_2 = SiyerResources.achievement_kemankes_2;
    private const string Kemankes_3 = SiyerResources.achievement_kemankes_3;

    private const string Muallim = SiyerResources.achievement_muallim;
    private const string Muallim_2 = SiyerResources.achievement_muallim_2;


    private const string Muhacir = SiyerResources.achievement_muhacir;


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
            case Building.Kabe: return Kabe;
            case Building.Abdulmuttalib: return Abdulmuttalib;
            case Building.HzMuhammed: return HzMuhammed;
            case Building.DarulErkam: return DarulErkam;
            case Building.Hamza: return Hamza;
            case Building.Omer: return Omer;
            case Building.Ebubekir: return Ebubekir;
            case Building.Hatice: return Hatice;
            case Building.EbuTalib: return EbuTalib;
            case Building.Muhacir: return Muhacir;
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
            case Building.DarulErkam: return Alim;
            case Building.EbuTalib: return Okcubasi;
            case Building.Omer: return Ustad;
            default:
                throw new ArgumentOutOfRangeException("building", building, null);
        }
    }

    public static bool IsBadge(string achievementId)
    {
        return !IsBuilding(achievementId);
    }

    public static bool IsBuilding(string achievementId)
    {
        switch (achievementId)
        {
            case Kabe: return true;
            case Abdulmuttalib: return true;
            case HzMuhammed: return true;
            case DarulErkam: return true;
            case Hamza: return true;
            case Omer: return true;
            case Ebubekir: return true;
            case Hatice: return true;
            case EbuTalib: return true;
            default: return false;
        }
    }

    public static List<string> AllAchievements()
    {
        var allAchievements = new List<string>
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
            Muhafiz,
            Alim,
            Okcubasi,
            Ustad,
            MEKKE_MUHAFIZLARI,
            ILIM_AVCILARI,
            CENGAVERLER,
            KAHRAMANLAR,
            PEYGAMBER_DOSTLARI,
            Kasif,
            Kasif_2,
            Kasif_3,
            Kasif_4,
            Kasif_5,
            Talebe,
            Talebe_2,
            Talebe_3,
            Talebe_4,
            Kemankes,
            Kemankes_2,
            Kemankes_3,
            Muallim,
            Muallim_2,
            Muhacir
        };

        return allAchievements;
    }

    public static BadgeManager.Badge ToBadge(string id)
    {
        switch (id)
        {
            case Kabe: return BadgeManager.Badge.Kabe;
            case Abdulmuttalib: return BadgeManager.Badge.Abdulmuttalib;
            case HzMuhammed: return BadgeManager.Badge.HzMuhammed;
            case DarulErkam: return BadgeManager.Badge.DarulErkam;
            case Hamza: return BadgeManager.Badge.Hamza;
            case Omer: return BadgeManager.Badge.Omer;
            case Ebubekir: return BadgeManager.Badge.Ebubekir;
            case Hatice: return BadgeManager.Badge.Hatice;
            case EbuTalib: return BadgeManager.Badge.EbuTalib;
            case Muhafiz: return BadgeManager.Badge.Muhafiz;
            case Alim: return BadgeManager.Badge.Alim;
            case Okcubasi: return BadgeManager.Badge.Okcubasi;
            case Ustad: return BadgeManager.Badge.Ustad;
            case MEKKE_MUHAFIZLARI: return BadgeManager.Badge.MEKKE_MUHAFIZLARI;
            case ILIM_AVCILARI: return BadgeManager.Badge.ILIM_AVCILARI;
            case CENGAVERLER: return BadgeManager.Badge.CENGAVERLER;
            case KAHRAMANLAR: return BadgeManager.Badge.KAHRAMANLAR;
            case PEYGAMBER_DOSTLARI: return BadgeManager.Badge.PEYGAMBER_DOSTLARI;
            case Kasif:
            case Kasif_2:
            case Kasif_3:
            case Kasif_4:
            case Kasif_5: return BadgeManager.Badge.Kasif;
            case Talebe:
            case Talebe_2:
            case Talebe_3:
            case Talebe_4: return BadgeManager.Badge.Talebe;
            case Kemankes:
            case Kemankes_2:
            case Kemankes_3: return BadgeManager.Badge.Kemankes;
            case Muallim:
            case Muallim_2: return BadgeManager.Badge.Muallim;
            case Muhacir: return BadgeManager.Badge.Muhacir;
            default:
                throw new ArgumentOutOfRangeException(id + " id is not known");
        }
    }

    public static string TitleOf(string id)
    {
        switch (id)
        {
            case Kabe: return "Kabe";
            case Abdulmuttalib: return "Abdulmuttalib";
            case HzMuhammed: return "HzMuhammed";
            case DarulErkam: return "DarulErkam";
            case Hamza: return "Hamza";
            case Omer: return "Omer";
            case Ebubekir: return "Ebubekir";
            case Hatice: return "Hatice";
            case EbuTalib: return "EbuTalib";
            case Muhafiz: return "Muhafiz";
            case Alim: return "Alim";
            case Okcubasi: return "Okcubasi";
            case Ustad: return "Ustad";
            case MEKKE_MUHAFIZLARI: return "MEKKE_MUHAFIZLARI";
            case ILIM_AVCILARI: return "ILIM_AVCILARI";
            case CENGAVERLER: return "CENGAVERLER";
            case KAHRAMANLAR: return "KAHRAMANLAR";
            case PEYGAMBER_DOSTLARI: return "PEYGAMBER_DOSTLARI";
            case Kasif:   return "Kasif 1";
            case Kasif_2: return "Kasif 2";
            case Kasif_3: return "Kasif 3";
            case Kasif_4: return "Kasif 4";
            case Kasif_5: return "Kasif 5";
            case Talebe: return "Talebe 1";
            case Talebe_2: return "Talebe 2";
            case Talebe_3: return "Talebe 3";
            case Talebe_4: return "Talebe 4";
            case Kemankes:return "Kemankes 1";
            case Kemankes_2:return "Kemankes 2";
            case Kemankes_3: return "Kemankes 3";
            case Muallim: return "Muallim 1";
            case Muallim_2: return "Muallim 2";
            case Muhacir: return "Muhacir";
            default:
                throw new ArgumentOutOfRangeException(id + " id is not known");
        }
    }
}