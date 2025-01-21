namespace Otodom.DTO
{
    public class Ogloszenieznieruchomoscia
    {
        public string Tytul { get; set; } = null!;
        public DateTime DataDodania { get; set; }
        public bool Status { get; set; }
        public string Opis { get; set; } = null!;
        public decimal Cena { get; set; }
        public int KlientIdKlienta { get; set; }
        public string Wojewodztwo { get; set; } = null!;
        public string Miasto { get; set; } = null!;
        public decimal KodPocztowy { get; set; }
        public string Ulica { get; set; } = null!;
        public decimal NrDomu { get; set; }
        public decimal PowierzchniaDomu { get; set; }
        public decimal LiczbaPieter { get; set; }
        public decimal RokBudowy { get; set; }
        public string StanWykonczenia { get; set; } = null!;
        public string RodzajOkna { get; set; } = null!;
        public string TypOgrzewania { get; set; } = null!;
        public string RodzajZabudowy { get; set; } = null!;
        public List<string> Zdjecie { get; set;}
    }
}
