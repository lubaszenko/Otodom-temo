namespace Otodom.DTO
{
    public class NieruchomoscRequest
    {
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
    }
}
