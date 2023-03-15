using System;
using System.Threading;
using UcakRez;

namespace UcakRez.RezProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            var ucak = new Ucak();
            Menu(ucak);

        }

        static void Menu(Ucak ucak)
        {
            bool devam = true;
            while (devam)
            {
                Console.Clear();
                Console.WriteLine("Uçak Rezervasyon Sistemi v1.0");
                Console.WriteLine();
                Console.WriteLine("Seçim yapınız.");
                Console.WriteLine("R: Rezervasyon");
                Console.WriteLine("D: Doğrulama");
                Console.WriteLine("Çıkış (ESCAPE)");

                ConsoleKey giris = Console.ReadKey().Key;

                switch (giris)
                {
                    case ConsoleKey.R:
                        Console.Clear();
                        string koltukSinifSecim = MenuKoltukSinif(); // E B
                        switch (koltukSinifSecim)
                        {
                            case "B":
                                Rezervasyon(ucak, RezTip.Bus);
                                break;
                            case "E":
                                Rezervasyon(ucak, RezTip.Eco);         
                                break;
                            default:
                                Console.WriteLine("Geçersiz giriş");
                                Thread.Sleep(1000);
                                break;
                        }
                        break;
                    case ConsoleKey.D:
                        Console.Clear();
                        Dogrulama(ucak);
                        break;
                    case ConsoleKey.A:
                        UcakGoster(ucak);
                        Console.ReadKey();
                        break;
                    case ConsoleKey.Escape:
                        devam = false;
                        break;
                    default:
                        Console.WriteLine("Geçersiz seçim");
                        Thread.Sleep(1500);
                        break;
                }
            }
        }

        private static string MenuKoltukSinif()
        {
            Console.WriteLine("Koltuk Sınıfları");
            Console.WriteLine("E: Economy");
            Console.WriteLine("B: Business");
            Console.Write("Hangi tip koltuk : ");
            return Console.ReadKey().Key.ToString().ToUpper();
        }


        #region Islemler

        private static void Dogrulama(Ucak ucak)
        {
            Console.WriteLine("Rezervasyon Doğrulama");
            if (KoltukGiris(ucak, out Koltuk girilenKoltuk))
            {
                var koltuk = ucak.KoltukGetir(girilenKoltuk.Sira, girilenKoltuk.Blok);
                if (koltuk != null && koltuk.Durum == KoltukDurum.Dolu)
                {
                    Yardimcilar.Bildirim(BildirimTip.Olumlu, "Rezervasyon Geçerli");

                    Console.WriteLine(koltuk);
                    Console.WriteLine(koltuk.Yolcu);
                    Yardimcilar.Bekletme();

                }
                else
                {
                    Yardimcilar.Bildirim(BildirimTip.Olumsuz, "Rezervasyon GEÇERSİZ");
                    Yardimcilar.Bekletme();
                }
            }
        }
        private static void Rezervasyon(Ucak ucak, RezTip rezTip)
        {
            Console.WriteLine("REZERVASYON");

            // Koltuk Secim (mutlaka bilgiyi almalı ya da ESCAPE)
            // Koltuk uygun
            // Yolcu bilgileri al
            // Yolcu Yerlestir

            Koltuk secilenKoltukBilgisi;
            if (KoltukSecim(ucak, rezTip, out secilenKoltukBilgisi))
            {
                if (ucak.KoltukUygun(secilenKoltukBilgisi))
                {
                    Yolcu yerlestirilecekYolcu = YolcuBilgilerGiris();
                    if (yerlestirilecekYolcu != null)
                    {
                        ucak.YolcuYerlestir(secilenKoltukBilgisi, yerlestirilecekYolcu);
                        RezervasyonBilgileriYazdir(yerlestirilecekYolcu, secilenKoltukBilgisi);
                    }
                }
                else
                {
                    Yardimcilar.Bildirim(BildirimTip.Uyari, "Koltuk dolu. Başka bir koluk seçiniz.");
                    Yardimcilar.Bekletme();
                }
            }
        }
        private static void UcakGoster(Ucak ucak)
        {
            ucak.KoltukTabloGoster(RezTip.Genel);
            // Console.WriteLine($"Doluluk : {ucak.Doluluk.ToString("P")}");
            Console.WriteLine("Devam için bir tuşa basınız");
            Console.ReadKey();
        }


        #endregion

        private static Yolcu YolcuBilgilerGiris()
        {
            string ad;
            string soyad;
            string pasaportNo;

            while (true)
            {
                try
                {
                    Console.WriteLine("Ad, Soyad ve Pasaport bilgilerini giriniz.\nİşlem iptal için (x)");
                    Console.Write("Adınız : ");
                    ad = Console.ReadLine();
                    if (ad.Trim() == "x") return null;
                    if (ad.Trim() == "") throw new Exception();

                    Console.Write("Soyadınız: ");
                    soyad = Console.ReadLine();
                    if (soyad.Trim() == "") throw new Exception();

                    Console.Write("Pasaport no: ");
                    pasaportNo = Console.ReadLine();
                    if (pasaportNo.Trim() == "") throw new Exception();

                    Yolcu yolcuBilgisi = new Yolcu(ad, soyad, pasaportNo);
                    return yolcuBilgisi;
                }
                catch
                {
                    Yardimcilar.Bildirim(BildirimTip.Olumsuz, "Boş bilgiler yer alıyor.");
                    Yardimcilar.Bekletme();
                }
            }
        }

        private static void RezervasyonBilgileriYazdir(Yolcu yolcu, Koltuk koltuk)
        {
            Console.WriteLine("================================");
            Console.WriteLine("Rezervasyon Bilgileri");
            Console.WriteLine("================================");
            Console.WriteLine($"Yolcu : {yolcu}");
            Console.WriteLine($"Koltuk : {koltuk}");
            Console.WriteLine("================================");
            Console.WriteLine("Devam için bir tuşa basınız");
            Console.ReadKey();
        }

        private static bool KoltukSecim(Ucak ucak, RezTip tip, out Koltuk koltukBilgisi)
        {
            while (true)
            {
                Console.Clear();
                ucak.KoltukTabloGoster(tip);
                Console.WriteLine("Koltuk seçiniz. İşlem iptal için (x)");

                if (KoltukGiris(ucak, out koltukBilgisi))
                {
                    if (tip == RezTip.Bus && koltukBilgisi.Sira <= ucak.MaxBus)
                    {
                        koltukBilgisi = new Koltuk(koltukBilgisi.Sira, koltukBilgisi.Blok);
                        koltukBilgisi.Sinif = KoltukSinif.Business;
                        return true;
                    }

                    if (tip == RezTip.Eco && koltukBilgisi.Sira > ucak.MaxBus)
                    {
                        koltukBilgisi = new Koltuk(koltukBilgisi.Sira, koltukBilgisi.Blok);
                        koltukBilgisi.Sinif = KoltukSinif.Economy;
                        return true;
                    }

                    Yardimcilar.Bildirim(BildirimTip.Olumsuz, "Seçtiğiniz sınıfta bu koltuk yer almıyor.");
                    Yardimcilar.Bekletme();
                }
                else return false;
            }
        }

        private static bool KoltukGiris(Ucak ucak, out Koltuk koltukGirisBilgisi)
        {
            while (true)
            {
                koltukGirisBilgisi = null; // ÇIKIŞ
                string giris = Console.ReadLine();

                if (giris == "x") return false;

                try
                {
                    if (giris.Length != 2) throw new Exception();

                    string girisSira = giris.Substring(0, 1);
                    string girisBlok = giris.Substring(1, 1).ToUpper();

                    int koltukSira = Convert.ToInt32(girisSira);
                    int koltukBlok = Yardimcilar.HarfBlokNo(girisBlok[0]);

                    if (
                        koltukSira > 0 &&
                        koltukSira <= ucak.MaxSira &&
                        koltukBlok > 0 &&
                        koltukBlok <= ucak.MaxBlok
                        )
                    {
                        koltukGirisBilgisi = new Koltuk(koltukSira, koltukBlok);
                        return true;
                    }

                    else throw new Exception();
                }
                catch (Exception)
                {
                    Yardimcilar.Bildirim(BildirimTip.Olumsuz, "Geçersiz giriş.(Geçerli girişler : 1A, 6C)");
                    Yardimcilar.Bekletme();
                }
            }
        }
        /*
        Menu
               Rezr
                   Bus
                   Eco
               Dog
               Ucak Göster
               Cıkış

        Kullanıcı giriş
        Rezervasyon
        Doğrulama
        Ucak Goster [Admin]

*/


    }
}
