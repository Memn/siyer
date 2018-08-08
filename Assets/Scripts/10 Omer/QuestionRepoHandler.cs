using System.Collections.Generic;
using UnityEngine;

public class QuestionRepoHandler : MonoBehaviour
{
    private static List<Question> fourth
    {
        get
        {
            return new List<Question>
            {
                new Question
                {
                    _answer = 1,
                    Text =
                        "Peygamber Efendimize Peygamberlik vazifesi gelmeden evvel, insanlar tarafından ................. olarak biliniyordu.",
                    Choices = new[] {"En güçlü kişi", "Muhammed'ül emîn", "Kureyşin ileri gelenlerinden"}
                },
                new Question
                {
                    _answer = 0,
                    Text =
                        "Hicret yolculuğunda Hz. Ali'yi yatağında bırakan Efendimiz, daha önce kendisine verilen ..........   ......... de teslim etmek üzere Hz. Ali'ye bırakmıştı.",
                    Choices = new[] {"müşriklerin emanetlerini", "Mekke’deki evini"}
                },
                new Question
                {
                    _answer = 1,
                    Text =
                        " Efendimiz yemeğe başlarken ..................., bitirdiğinde ise ..................... derdi.",
                    Choices = new[] {"Allahuekber - Maşallah", "Bismillah - Elhamdülillah", "Maşallah - Barekallah"}
                },
                new Question
                {
                    _answer = 1,
                    Text =
                        "Efendimiz, işkence edip, hakaretler eden müşriklere karşı, Sahabilere nasıl bir tavır sergilemelerini söylemiştir?",
                    Choices = new[]
                    {
                        "Onlara misliyle cevap verin.", "İyiliğin en güzeliyle cevap verin.",
                        "Onları Allah'a havale ederek beddualar edin."
                    }
                },
                new Question
                {
                    _answer = 0,
                    Text = "Hz. Hatice Efendimizin hangi özelliğinden dolayı onunla ticaret yapmayı istemiştir?",
                    Choices = new[]
                    {
                        "Dürüstlüğü ve güvenilir oluşu", "Peygamber oluşu", "Kendisine Kur'an'ın inmesi",
                        "Mucize sahibi oluşu"
                    }
                },
                new Question
                {
                    _answer = 0,
                    Text =
                        "Efendimizin amcası olan Ebu Talib, ticaret yapmak üzere Şam'a giderken daha 12 yaşlarında olan Efendimizi de yanına almıştır. Ebu Talib'in Efendimizi yanına almasının sebebi nedir?",
                    Choices = new[] {"Yetim oluşu", "Dürüstlüğü", "Amcasına yardım etmek istemesi", "Güvenilir oluşu"}
                },
                new Question
                {
                    _answer = 2,
                    Text =
                        "Peygamber Efendimiz, gençliğinde Mekke'deki ahlaksızlıklarla, yolsuzluk ve kötülüklerle mücadeleye destek vermek için, ..................... cemiyetine girmiştir.",
                    Choices = new[] {"Fedâiler", "Darul Erkam", "Hılful fudul"}
                },
                new Question
                {
                    _answer = 0,
                    Text =
                        "Peygamber Efendimiz müşriklere hitaben:  \"Sizi bir kelimeyi söylemeye çağırıyorum.\" \nMüşrikler: \"Nedir o?\"\n Peygamber Efendimiz: \"....................................\"\nMüşrikler: \"Sen bizim 300 tanrımızı bire mi indirdin Muhammed!\"",
                    Choices = new[] {"La ilahe illallah", "Bismillahirrahmanirahim", "Maşallah", "İnşallah"}
                },
                new Question
                {
                    _answer = 2,
                    Text =
                        "Ahmet, arkadaşı Orhan'la yemek yiyecekti. Yemeğe başlamadan önce Orhan besmele çekmeyi unutmuş, Ahmet ise onu uyarmak için sesli bir şekilde besmele çekmişti. Hep birlikte gülüşerek yemeğe başladılar.\n" +
                        "Yukarıdaki hâdisede Ahmet, Efendimizin hangi tavsiyesini uygulamıştır?",
                    Choices = new[]
                    {
                        "Namaz, müminin miracıdır.", "Temizlik, imandandır.",
                        "Mümin kişi, diğer mümine karşı duvar gibidir. Birbirlerini takviye ederler."
                    }
                },
                new Question
                {
                    _answer = 1,
                    Text =
                        "I) Bu hareketi çocukları gayrete getirir, İslam'ın emirlerini öğrenmek için birbirleriyle yarışırlardı.\n" +
                        "II)Onları büyük bir insan gibi karşısına alır, ciddiyetle dinlerdi. \n" +
                        "III) Efendimiz çocukları çok severdi.\n" +
                        "Yukarıdaki paragrafı anlamlı hale gelecek şekilde düzenleyiniz.",
                    Choices = new[] {"I-III-II", "III-II-I", "II-III-I"}
                },
            };
        }
    }

    private static List<Question> fifth
    {
        get
        {
            return new List<Question>
            {
                new Question
                {
                    _answer = 0,
                    Text =
                        "Hatice'nin arkadaşı eline annesinin yaptığı kurabiyeleri almış, iştahlı bir şekilde yiyordu. Hatice çok acıkmış, fakat arkadaşından istemesine rağmen arkadaşı kurabiyesini paylaşmak istememişti. Hatice’nin arkadaşına bu olaydan sonraki tavrı," +
                        " Efendimizin hangi hadisi şerifi doğrultusunda olmalıdır?",
                    Choices = new[]
                    {
                        "Sana vermeyene verirsin", "Kolaylaştırın, güçleştirmeyin.",
                        "Utanmadıktan sonra dilediğini yap."
                    }
                },
                new Question
                {
                    _answer = 2,
                    Text =
                        "Efendimiz, Cebrail (as) ile başladığı yolculuğuna, bir noktadan sonra yalnız devam ederek Allah'ın huzuruna tek kabul edilmiştir. Bu yolculuğun adı nedir?",
                    Choices = new[] {"Hicret", "Mahşer", "Mirac ", "Sırat"}
                },
                new Question
                {
                    _answer = 0,
                    Text =
                        " Peygamber Efendimiz, Miracta, bütün mahlûkatın selamlarını Allah'a arz etmiştir. Ve Allah'ın selamını da bütün salih kullar namına almıştır. Bu ulvi konuşma, hangi dua da geçmektedir?",
                    Choices = new[] {"Tahıyyat", "Subhaneke", "Ezan duası", "Kunut duası"}
                },
                new Question
                {
                    _answer = 2,
                    Text = " Miracta Rabbimiz, Peygamberimize nasıl selam vermiştir?",
                    Choices = new[]
                    {
                        "Elhamdülillahi Rabbil Alemin", "Bismillahirrahmanirrahim", "Esselamü Aleyke Yâ Eyyühennebiyyü"
                    }
                },
                new Question
                {
                    _answer = 3,
                    Text =
                        " Efendimize gelen ilk vahiy \"Oku\" emridir. Peki, Efendimiz okuma-yazma bilmediği halde, neden böyle bir emir gelmiş olabilir?",
                    Choices = new[]
                    {
                        "Kâinatı bir kitap gibi görmesi için", "Kâinatta Allah'ın isimlerini okumak için",
                        "Kur'an ayetlerini okuması için", "Hepsi"
                    }
                },
                new Question
                {
                    _answer = 0,
                    Text =
                        "\"Peygamberimiz, doğruluk ve dürüstlüğün en güzel örneği idi. ......................................... , hiç yalan söylememiştir. Peygamberliğinden önceki gençlik döneminde doğruluğu ve güvenilir kişiliğinden dolayı kendisine, “Muhammedü’l-Emin”  deniyordu.\" Paragraftaki eksik cümleyi sürükleyiniz.",
                    Choices = new[]
                    {
                        "O, çocukluğundan itibaren doğruluktan ayrılmamış",
                        "Onun, peygamber olduktan sonra doğruluk rehberi olmuş.",
                        "Çocukken hep adaletli ve güler yüzlüymüş."
                    }
                },
                new Question
                {
                    _answer = 3,
                    Text =
                        " Kâinattaki her çeşit varlığın, Efendimizin Peygamberliğini kabul ettiğini mucizelerle anlıyoruz. Aşağıdakilerden hangisini buna örnek olarak gösterebiliriz?",
                    Choices = new[]
                    {
                        "İşaretiyle ayın ikiye bölünmesi", "Elinde taşların zikretmesi",
                        "Çağırdığında ağaçların yanına gelmesi", "Hepsi"
                    }
                },
                new Question
                {
                    _answer = 2,
                    Text = " Peygamberler neden mucize göstermişler?",
                    Choices = new[]
                    {
                        "Küfür Ehlinin inadını kırmak için", "İmanlı kimselerin imanını artırmak için", "İkisi de",
                        "Hiçbiri"
                    }
                },
                new Question
                {
                    _answer = 2,
                    Text = " I) Efendimize \"oku\" ayetinin inişi\n" + "II) Hz. Hatice ile Efendimizin evlenmesi\n" +
                           "III) Rahip Bahira'nın Efendimizi görmesi\n" +
                           "IV) İlk Müslüman çocuk olan Hz. Ali'nin Müslüman olması\n" +
                           "Yukarıdaki maddeleri kronolojik sıraya göre sıralayınız.",
                    Choices = new[] {"IV-III-II-I", "I-IV-III-II", "III-II-I-IV", "II-III-I-IV"}
                },
            };
        }
    }

    public static List<Question> Questions(int level)
    {
        switch (level)
        {
            case 4:  return fourth;
            case 5:  return fifth;
            default: return null;
        }
    }
}