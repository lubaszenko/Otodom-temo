namespace ApiConsumer.DTO
{
    public class KlientRequest
    {
        public string Imie { get; set; } = null!;
        public string Nazwisko { get; set; } = null!;
        public string Email { get; set; } = null!;
        public decimal NrTelefonuKlienta { get; set; }
        public int? AgencjaIdAgencji { get; set; }
    }
}
