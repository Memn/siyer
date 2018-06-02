using System;
using System.Collections.Generic;
using UnityEngine;

public class CommonResources : MonoBehaviour
{
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
    private static string Kabe { get; set; }
    private static string Abdulmuttalib { get; set; }
    private static string HzMuhammed { get; set; }
    private static string DarulErkam { get; set; }
    private static string Hamza { get; set; }
    private static string Omer { get; set; }
    private static string Ebubekir { get; set; }
    private static string Hatice { get; set; }
    private static string EbuTalib { get; set; }

    // Rozetler
    private static string Muhafiz { get; set; }
    private static string Talebe { get; set; }
    private static string Okcubasi { get; set; }
    private static string Muallim { get; set; }

    // Levels
    private static string MEKKE_MUHAFIZLARI{ get; set; }
    private static string ILIM_AVCILARI { get; set; }
    private static string CENGAVERLER { get; set; }
    private static string KAHRAMANLAR { get; set; }
    private static string PEYGAMBER_DOSTLARI { get; set; }


    public enum Resource
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

    public static readonly Dictionary<Resource, Description> Descriptions = new Dictionary<Resource, Description>
    {
        {
            Resource.Kabe,
            new Description
            {
                Title = "Kâbe",
                Info = "Allah katında çok kıymetli olan Kâbe, " +
                       "Hz. İbrahim tarafından inşa edildiği zamandan itibaren insanlar " +
                       "tarafından da daima kutsal kabul edilmiştir. Müslümanların kıblesidir."
            }
        },
        {
            Resource.Abdulmuttalib,
            new Description
            {
                Title = "Abdulmuttalib'in Evi",
                Info = "Peygamberimizin dedesi olan Abdulmuttalib, Mekke'nin Reisidir. " +
                       "Peygamberimiz annesinin vefatından sonra iki sene kadar " +
                       "dedesi Abdulmuttalib'in himayesinde kalmıştır."
            }
        },
        {
            Resource.HzMuhammed,
            new Description
            {
                Title = "Peygamberimizin Evi",
                Info = "571 yılında Mekke’de doğdu. O’nun ahlakı, Kur'an ahlakıdır. " +
                       "Hz. Muhammed (s.a.v.) son peygamberdir."
            }
        },
        {
            Resource.DarulErkam,
            new Description
            {
                Title = "Darul Erkam",
                Info = "İslamiyet’in ilk yıllarında henüz gizli davet yapılırken, Hz. Erkam evini bağışlamış; " +
                       "Darul Erkam İslamiyet’in ilk medresesi olmuştur."
            }
        },
        {
            Resource.Hamza,
            new Description
            {
                Title = "Hz. Hamza’nın Evi",
                Info = "Kureyşin en şereflilerinden olan Hz. Hamza, iyi bir avcı, keskin bir nişancıdır. " +
                       "Peygamber Efendimizin amcasıdır."
            }
        },
        {
            Resource.Omer,
            new Description
            {
                Title = "Hz. Ömer’in Evi",
                Info = "İslam'ın ikinci halifesi olan Hz. Ömer, nüfuzuyla güç ve kuvvetiyle meşhurdu. " +
                       "Onun iman etmesi Müslümanlara büyük bir kuvvet kazandırdı. " +
                       "Hatta Müslüman olduğu gün bütün Müslümanlar Kâbe’ye giderek ilk defa açıktan namaz kıldılar."
            }
        },
        {
            Resource.Ebubekir,
            new Description
            {
                Title = "Hz. Ebu Bekir'in Evi",
                Info = "Hz. Ebu Bekir, Peygamber Efendimizin en yakın dostlarındandır. " +
                       "Hatta hicrette de yoldaşı olmuştur. Bir an bile tereddüt etmeden Müslüman olan " +
                       "Hz. Ebu Bekir aynı zamanda İslamiyet’in ilk halifesidir."
            }
        },
        {
            Resource.Hatice,
            new Description
            {
                Title = "Hz. Hatice’nin Evi",
                Info = "Yeryüzünde İslam'a ilk inanan insan olan Hz. Hatice, Efendimizin de ilk eşidir. " +
                       "Efendimizin Peygamberliğini ilk tasdik eden ve her türlü sıkıntıda daima yanında olan " +
                       "Hz. Hatice’nin vefatı hüzün yılı olarak adlandırılmıştır."
            }
        },
        {
            Resource.EbuTalib,
            new Description
            {
                Title = "Ebu Talib'in Evi",
                Info = "İlk Müslüman çocuk olan Hz. Ali’nin babası, Efendimizin amcasıdır. " +
                       "Peygamber Efendimiz sekiz yaşındayken dedesi vefat edince, amcası " +
                       "Ebu Talib'in himayesinde kalmıştır."
            }
        }
    };

    public static readonly Dictionary<int, List<Duty>> Duties = new Dictionary<int, List<Duty>>
    {
        {
            1,
            new List<Duty>
            {
                new Duty
                {
                    Resource = Resource.Kabe,
                    Requirement = 0,
                    Title = "Fil Vakasi animasyonlarini bitir.",
                    Reward = Abdulmuttalib
                },
                new Duty
                {
                    Resource = Resource.Abdulmuttalib,
                    Requirement = 10,
                    Title = "Labirent 10 deveyi verilen surede bul.",
                    Reward = Muhafiz
                },
            }
        },
        {
            2,
            new List<Duty>
            {
                new Duty {Resource = Resource.HzMuhammed, Requirement = 0, Title = "Animasyonlari bitir."},
                new Duty
                {
                    Resource = Resource.Abdulmuttalib,
                    Requirement = 10,
                    Title = "Labirent 10 deveyi verilen surede bul."
                    
                },
                new Duty
                {
                    Resource = Resource.DarulErkam,
                    Requirement = 0,
                    Title = "Seviyeye ait kelimeleri tek seferde bitir"
                },
            }
        },
        {
            3,
            new List<Duty>
            {
                new Duty {Resource = Resource.Hamza, Requirement = 0, Title = "Animasyonlari bitir."},
                new Duty
                {
                    Resource = Resource.Abdulmuttalib,
                    Requirement = 10,
                    Title = "Labirent 10 deveyi verilen surede bul."
                },
                new Duty
                {
                    Resource = Resource.DarulErkam,
                    Requirement = 0,
                    Title = "Seviyeye ait kelimeleri tek seferde bitir"
                },
                new Duty {Resource = Resource.Omer, Requirement = 6, Title = "10 Ok atisindan 6 tanesinde hedefi vur."},
            }
        },
        {
            4,
            new List<Duty>
            {
                new Duty {Resource = Resource.Ebubekir, Requirement = 0, Title = "Animasyonlari bitir."},
                new Duty
                {
                    Resource = Resource.Abdulmuttalib,
                    Requirement = 10,
                    Title = "Labirent 10 deveyi verilen surede bul."
                },
                new Duty
                {
                    Resource = Resource.DarulErkam,
                    Requirement = 0,
                    Title = "Seviyeye ait kelimeleri tek seferde bitir"
                },
                new Duty {Resource = Resource.Omer, Requirement = 6, Title = "10 Ok atisindan 6 tanesinde hedefi vur."},
                new Duty
                {
                    Resource = Resource.Hatice,
                    Requirement = 6,
                    Title = "Seviyeye ait sorulardan en az 6 tanesini bil."
                }
            }
        },
        {
            5,
            new List<Duty>
            {
                new Duty {Resource = Resource.EbuTalib, Requirement = 0, Title = "Animasyonlari bitir."},
                new Duty
                {
                    Resource = Resource.Abdulmuttalib,
                    Requirement = 10,
                    Title = "Labirent 10 deveyi verilen surede bul."
                },
                new Duty
                {
                    Resource = Resource.DarulErkam,
                    Requirement = 0,
                    Title = "Seviyeye ait kelimeleri tek seferde bitir"
                },
                new Duty {Resource = Resource.Omer, Requirement = 8, Title = "10 Ok atisindan 8 tanesinde hedefi vur."},
                new Duty
                {
                    Resource = Resource.Hatice,
                    Requirement = 8,
                    Title = "Seviyeye ait sorulardan en az 8 tanesini bil."
                }
            }
        }
    };

    public class Duty
    {
        public string Title;
        public int Requirement;
        public Resource Resource;
        public string Reward;
    }

    public class Description
    {
        public static readonly Description None = new Description {Title = "", Info = ""};
        public string Title;
        public string Info;
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