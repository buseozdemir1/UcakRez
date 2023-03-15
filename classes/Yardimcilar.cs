using System;
using System.Collections.Generic;
using System.Threading;

namespace UcakRez
{
    class Yardimcilar
    {
        private static List<char> blokHarfler = new List<char> {
            ' ','A','B','C','D','E','F'
        };

        public static int HarfBlokNo(char harf)
        {
            if (blokHarfler.Contains(harf))
            {
                return blokHarfler.IndexOf(harf);
            }
            throw new Exception();
        }

        public static char BlokNoHarf(int blok)
        {
            return blokHarfler[blok];
        }
        public static void Bekletme()
        {
            Thread.Sleep(2000);
            Console.Clear();
        }

        public static void Bildirim(BildirimTip tip, string mesaj)
        {
            ConsoleColor tipRenk;

            switch (tip)
            {
                case BildirimTip.Olumlu:
                    tipRenk = ConsoleColor.Blue;
                    break;
                case BildirimTip.Uyari:
                    tipRenk = ConsoleColor.Yellow;
                    break;
                case BildirimTip.Olumsuz:
                    tipRenk = ConsoleColor.Red;
                    break;
                default:
                    tipRenk = ConsoleColor.DarkGreen;
                    break;
            }

            Console.BackgroundColor = tipRenk;
            Console.WriteLine($" {mesaj} ");
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}