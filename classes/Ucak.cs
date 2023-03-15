using System;
using System.Collections.Generic;

namespace UcakRez
{
    class Ucak
    {
        public int MaxSira { get; set; }
        public int MaxBlok { get; set; }
        public int MaxBus { get; set; }

        public double Doluluk
        {
            get
            {
                int sayac = 0;
                foreach (var k in _koltuklar)
                {
                    if (k.Durum == KoltukDurum.Dolu)
                    {
                        sayac++;
                    }
                }
                // return sayac / 19f;
                return (double)sayac / (MaxSira * MaxBlok);
                // return 0.5m;
            }
        }

        private List<Koltuk> _koltuklar = new List<Koltuk>();

        public Ucak(int maxSira = 8, int maxBlok = 5, int maxBus = 5)
        {
            MaxSira = maxSira;
            MaxBlok = maxBlok;
            MaxBus = maxBus;
            // Koltukları oluştur
            KoltuklarOlustur();
        }

        private void KoltuklarOlustur()
        {
            for (int i = 0; i < MaxSira; i++)
            {
                for (int j = 0; j < MaxBlok; j++)
                {
                    var koltuk = new Koltuk(i + 1, j + 1);
                    _koltuklar.Add(koltuk);
                }
            }
        }

        public void KoltukTabloGoster(RezTip tip)
        {
            for (int i = 0; i < MaxSira; i++)
            {
                // Baslık
                if (i == 0)
                {
                    for (int k = 0; k <= MaxBlok; k++)
                    {
                        Console.Write($"{Yardimcilar.BlokNoHarf(k)}   ");
                    }
                    Console.WriteLine();
                }
                // Satirlar tablo tipine göre

                if (
                        (tip == RezTip.Bus && i < MaxBus) ||
                        (tip == RezTip.Eco && i >= MaxBus) ||
                        (tip == RezTip.Genel)
                        )
                {
                    Console.Write($"{i + 1} ");
                    for (int j = 0; j < MaxBlok; j++)
                    {
                        var koltuk = _koltuklar[i * MaxBlok + j];
                        koltuk.KoltukBilgiYazdir();
                        Console.Write(" ");
                        // Console.BackgroundColor = koltuk.Durum == KoltukDurum.Bos ? ConsoleColor.Blue : ConsoleColor.Red;
                        // Console.Write($"{koltuk.Sira}{Yardimcilar.BlokNoHarf(koltuk.Blok)}  ");
                        // Console.BackgroundColor = ConsoleColor.Black;
                    }
                }
                //
                Console.WriteLine();
            }
            KoltukRehber();
        }

        // Koltuğa Yolcu Yerlestir
        public void YolcuYerlestir(Koltuk koltuk, Yolcu yolcu)
        {
            foreach (var k in _koltuklar)
            {
                if (k.Sira == koltuk.Sira && k.Blok == koltuk.Blok)
                {
                    k.Yolcu = yolcu;
                    k.Durum = KoltukDurum.Dolu;
                    return;
                }
            }
            throw new Exception();
        }
        /// <summary>
        /// Koltuk durumuna göre True/False döndürür.
        /// Koltuk bulunamaz ise hata verir.
        /// </summary>
        /// <param name="koltuk"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool KoltukUygun(Koltuk koltuk)
        {
            foreach (var k in _koltuklar)
            {
                if (
                    k.Sira == koltuk.Sira &&
                    k.Blok == koltuk.Blok
                    )
                {
                    return k.Durum == KoltukDurum.Dolu ? false : true;
                }
            }

            // return false;
            throw new Exception();
        }

        /// <summary>
        /// Koltuk bulunamaz ise null döndürür.
        /// </summary>
        /// <param name="sira"></param>
        /// <param name="blok"></param>
        /// <returns></returns>
        public Koltuk KoltukGetir(int sira, int blok)
        {
            foreach (var k in _koltuklar)
            {
                if (k.Sira == sira && k.Blok == blok)
                {
                    return k;
                }
            }

            return null;
        }

        public void YolcuListele()
        {
            // for (int i = 0; i < MaxSira; i++)
            // {
            //     for (int j = 0; j < MaxBlok; j++)
            //     {
            //         var k = _koltuklar[(i * j) + j];
            //         if ((k.Durum == KoltukDurum.Dolu))
            //         {
            //             Console.WriteLine($"{k.Sira}:{Yardimcilar.BlokNoHarf(k.Blok)} | {k.Durum} | {k.Sinif} | {k.Yolcu.Ad} | {k.Yolcu.Soyad} | {k.Yolcu.PasaportNo}");
            //         }
            //     }
            // }

            foreach (var k in _koltuklar)
            {
                if (k.Durum == KoltukDurum.Dolu)
                {
                    Console.WriteLine($"{k.Sira}:{Yardimcilar.BlokNoHarf(k.Blok)} | {k.Durum} | {k.Sinif} | {k.Yolcu.Ad} | {k.Yolcu.Soyad} | {k.Yolcu.PasaportNo}");
                }

            }
        }

        private void KoltukRehber()
        {
            Console.WriteLine("------------------------");
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Write(" ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(" Boş ");
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write(" ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine(" Dolu ");
            Console.WriteLine("------------------------");
        }

        #region Testing

        public void TestDoldur(params int[] degerler)
        {
            foreach (var deger in degerler)
            {
                if (deger < _koltuklar.Count)
                    _koltuklar[deger].Durum = KoltukDurum.Dolu;
            }
        }

        #endregion

    }
}
