using UnityEngine;

public class BadgeManager : MonoBehaviour
{
    public Sprite[] Badges;
    public Badge[] Order;

    public enum Badge
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

        Talebe,

        Kemankes,

        Muallim,

        Muhacir,
    }

    public Sprite SrpiteOf(string id)
    {
        var badge = CommonResources.ToBadge(id);
        for (var i = 0; i < Order.Length; i++)
        {
            if (Order[i] == badge)
            {
                return Badges[i];
            }
        }

        return null;
    }

    public string TitleOf(string id)
    {
        return CommonResources.TitleOf(id);
    }
}