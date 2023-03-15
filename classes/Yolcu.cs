namespace UcakRez
{
    class Yolcu
    {
        public string Ad { get; set; } = string.Empty;
        public string Soyad { get; set; } = string.Empty;
        public string PasaportNo { get; set; }

        public Yolcu(string ad, string soyad, string pasaportNo)
        {
            Ad = ad;
            Soyad = soyad;
            PasaportNo = pasaportNo;
        }

        public override string ToString()
        {
            return $"{Ad} {Soyad} [{PasaportNo}]";
        }
    }
}
