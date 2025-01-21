namespace ApiConsumer.DTO
{
    public class OgloszenieResponse
    {
        public int Id { get; set; }
        public string Tytul { get; set; } = null!;
        public DateTime DataDodania { get; set; }
        public bool Status { get; set; }
        public string Opis { get; set; } = null!;
        public decimal Cena { get; set; }
        public int KlientIdKlienta { get; set; }
        public string ImieKlienta { get; set; }
        public string NazwiskoKlienta { get; set; }
        public string Email { get; set; }
        public decimal Telefon { get; set; }
        public NieruchomoscResponse Nieruchomosc { get; set; }
    }
}
