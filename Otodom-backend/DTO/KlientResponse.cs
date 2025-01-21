namespace Otodom.DTO
{
    public class KlientResponse
    {
        public int IdKlienta { get; set; }
        public string Imie { get; set; } = null!;
        public string Nazwisko { get; set; } = null!;
        public string Email { get; set; } = null!;
        public decimal NrTelefonuKlienta { get; set; }
        public string NazwaAgencji { get; set; } = null!;
    }
}
