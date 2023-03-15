using System;

namespace UcakRez
{
    class Koltuk
    {
        public int Sira { get; set; }
        public int Blok { get; set; }
        public KoltukDurum Durum { get; set; }
        public KoltukSinif Sinif { get; set; }
        public Yolcu Yolcu { get; set; }

        public Koltuk()
        {
            Durum = KoltukDurum.Bos;
            // Sinif = KoltukSinif.Kategorisiz;
        }

        public Koltuk(int sira, int blok)
        {
            Sira = sira;
            Blok = blok;
            Durum = KoltukDurum.Bos;
        }

        public Koltuk(int sira, char blok)
        {
            Sira = sira;
            Blok = Yardimcilar.HarfBlokNo(blok);
            Durum = KoltukDurum.Bos;
        }

        /// <summary>
        /// Koltuk kendi durumuna göre renk belirler.
        /// Konsola yazdırır. 
        /// ConsoleWrite() kullanır.
        /// </summary>
        public void KoltukBilgiYazdir()
        {
            ConsoleColor durumRenk = ConsoleColor.Black;
            switch (Durum)
            {
                case KoltukDurum.Bos:
                    durumRenk = ConsoleColor.Blue;
                    break;
                case KoltukDurum.Dolu:
                    durumRenk = ConsoleColor.Red;
                    break;
                case KoltukDurum.Notr:
                default:
                    durumRenk = ConsoleColor.Black;
                    break;
            }
            Console.BackgroundColor = durumRenk;
            Console.Write($" {Sira}{Yardimcilar.BlokNoHarf(Blok)} ");
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public override string ToString()
        {
            return $"{Sira}{Yardimcilar.BlokNoHarf(Blok)} [{Sinif}]";
        }
    }
}
