using System;
using System.Text;

class Kitap
{
    private string ad;
    private string yazar;
    private int sayfaSayisi;

    public string Ad
    {
        get { return ad; }
        set { ad = value; }
    }

    public string Yazar
    {
        get { return yazar; }
        set { yazar = value; }
    }

    public int SayfaSayisi
    {
        get { return sayfaSayisi; }
        set { sayfaSayisi = value > 0 ? value : 1; }
    }

    public bool OduncteMi { get; private set; }

    public Kitap(string ad, string yazar)
    {
        Ad = ad;
        Yazar = yazar;
        SayfaSayisi = 1;
        OduncteMi = false;
    }

    public Kitap(string ad, string yazar, int sayfaSayisi)
    {
        Ad = ad;
        Yazar = yazar;
        SayfaSayisi = sayfaSayisi;
        OduncteMi = false;
    }

    public virtual void BilgiGoster()
    {
        Console.WriteLine($"   Ad: {Ad}");
        Console.WriteLine($"   Yazar: {Yazar}");
        Console.WriteLine($"   Sayfa Sayısı: {SayfaSayisi}");
        Console.WriteLine($"   Durum: {(OduncteMi ? "Ödünç Verildi" : "Müsait")}");
    }

    public void OduncAl()
    {
        if (OduncteMi)
        {
            Console.WriteLine("Uyarı: Bu kitap zaten ödünç verilmiş!");
        }
        else
        {
            OduncteMi = true;
            Console.WriteLine($"'{Ad}' ödünç alındı.");
        }
    }

    public void IadeEt()
    {
        if (!OduncteMi)
        {
            Console.WriteLine("Uyarı: Bu kitap zaten kütüphanede.");
        }
        else
        {
            OduncteMi = false;
            Console.WriteLine($"'{Ad}' iade edildi.");
        }
    }
}

class Roman : Kitap
{
    public string RomanTuru { get; set; }

    public Roman(string ad, string yazar, int sayfaSayisi, string romanTuru)
        : base(ad, yazar, sayfaSayisi)
    {
        RomanTuru = romanTuru;
    }

    public override void BilgiGoster()
    {
        Console.WriteLine("   Tür: Roman");
        base.BilgiGoster();
        Console.WriteLine($"   Roman Türü: {RomanTuru}");
    }
}

class DersKitabi : Kitap
{
    public string DersKonusu { get; set; }

    public DersKitabi(string ad, string yazar, int sayfaSayisi, string dersKonusu)
        : base(ad, yazar, sayfaSayisi)
    {
        DersKonusu = dersKonusu;
    }

    public override void BilgiGoster()
    {
        Console.WriteLine("   Tür: Ders Kitabı");
        base.BilgiGoster();
        Console.WriteLine($"   Konu: {DersKonusu}");
    }
}

class BilimKitabi : Kitap
{
    public string BilimAlani { get; set; }

    public BilimKitabi(string ad, string yazar, int sayfaSayisi, string bilimAlani)
        : base(ad, yazar, sayfaSayisi)
    {
        BilimAlani = bilimAlani;
    }

    public override void BilgiGoster()
    {
        Console.WriteLine("   Tür: Bilim Kitabı");
        base.BilgiGoster();
        Console.WriteLine($"   Bilim Alanı: {BilimAlani}");
    }
}

class Program
{
    static Kitap[] kitaplar = new Kitap[20]; 
    static int kitapSayisi = 0;              

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        int secim = -1;
        while (secim != 0)
        {
            Console.WriteLine("\n===== KÜTÜPHANE SİSTEMİ =====\n");
            Console.WriteLine("1 - Kitap Ekle");
            Console.WriteLine("2 - Kitapları Listele");
            Console.WriteLine("3 - Kitap Ara");
            Console.WriteLine("4 - Kitap Ödünç Al");
            Console.WriteLine("5 - Kitap İade Et");
            Console.WriteLine("0 - Çıkış");

            secim = SayiOku("\nSeçiminiz: ");
            Console.WriteLine();

            switch (secim)
            {
                case 1: KitapEkle(); break;
                case 2: KitaplariListele(); break;
                case 3: KitapAra(); break;
                case 4: OduncIslemi(true); break;
                case 5: OduncIslemi(false); break;
                case 0: Console.WriteLine("Çıkış yapılıyor..."); break;
                default: Console.WriteLine("Geçersiz seçim!"); break;
            }
        }
    }
    static int SayiOku(string mesaj)
    {
        int sayi;
        Console.Write(mesaj);
        while (!int.TryParse(Console.ReadLine(), out sayi))
        {
            Console.Write("Lütfen sayı girin: ");
        }
        return sayi;
    }

    static void KitapEkle()
    {
        if (kitapSayisi >= kitaplar.Length)
        {
            Console.WriteLine("Kütüphane dolu! En fazla 20 kitap eklenebilir.");
            return;
        }

        Console.WriteLine("Kitap türü:");
        Console.WriteLine("1 - Roman");
        Console.WriteLine("2 - Ders Kitabı");
        Console.WriteLine("3 - Bilim Kitabı");
        int tur = SayiOku("Seçiminiz: ");

        if (tur < 1 || tur > 3)
        {
            Console.WriteLine("Geçersiz tür!");
            return;
        }

        Console.Write("Kitap adı: ");
        string ad = Console.ReadLine();
        Console.Write("Yazar: ");
        string yazar = Console.ReadLine();
        int sayfa = SayiOku("Sayfa sayısı: ");

        if (tur == 1)
        {
            Console.Write("Roman türü: ");
            kitaplar[kitapSayisi] = new Roman(ad, yazar, sayfa, Console.ReadLine());
        }
        else if (tur == 2)
        {
            Console.Write("Ders konusu: ");
            kitaplar[kitapSayisi] = new DersKitabi(ad, yazar, sayfa, Console.ReadLine());
        }
        else
        {
            Console.Write("Bilim alanı: ");
            kitaplar[kitapSayisi] = new BilimKitabi(ad, yazar, sayfa, Console.ReadLine());
        }

        kitapSayisi++;
        Console.WriteLine("Kitap eklendi.");
    }

    static void KitaplariListele()
    {
        if (kitapSayisi == 0)
        {
            Console.WriteLine("Kütüphanede kitap yok.");
            return;
        }

        for (int i = 0; i < kitapSayisi; i++)
        {
            Console.WriteLine($"{i + 1}.");
            kitaplar[i].BilgiGoster(); 
            Console.WriteLine();
        }
    }

    static void KitapAra()
    {
        Console.Write("Aranacak kitap adı: ");
        string aranan = Console.ReadLine().ToLower();
        bool bulundu = false;

        for (int i = 0; i < kitapSayisi; i++)
        {
            if (kitaplar[i].Ad.ToLower().Contains(aranan))
            {
                Console.WriteLine($"\n{i + 1}.");
                kitaplar[i].BilgiGoster();
                bulundu = true;
            }
        }

        if (!bulundu)
            Console.WriteLine("Eşleşen kitap bulunamadı.");
    }

    static void OduncIslemi(bool oduncAl)
    {
        Console.Write("Kitap adı: ");
        string ad = Console.ReadLine().ToLower();

        for (int i = 0; i < kitapSayisi; i++)
        {
            if (kitaplar[i].Ad.ToLower() == ad)
            {
                if (oduncAl)
                    kitaplar[i].OduncAl();
                else
                    kitaplar[i].IadeEt();
                return;
            }
        }

        Console.WriteLine("Kitap bulunamadı.");
    }
}