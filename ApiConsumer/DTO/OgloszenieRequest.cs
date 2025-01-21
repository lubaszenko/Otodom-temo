namespace ApiConsumer.DTO
{
    public class OgloszenieRequest
    {
        public string Tytul { get; set; } = null!;
        public DateTime DataDodania { get; set; }
        public bool Status { get; set; }
        public string Opis { get; set; } = null!;
        public decimal Cena { get; set; }
        public int KlientIdKlienta { get; set; }
        public int NieruchomoscIdNieruchomosci { get; set; }
    }
}
