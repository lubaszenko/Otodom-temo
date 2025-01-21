namespace Otodom.DTO
{
    public class AgencjaResponse
    {
        public string NazwaAgencji { get; set; } = null!;
        public decimal NrTelefonuAgencji { get; set; }
        public string Email { get; set; } = null!;
        public decimal Nip { get; set; }
    }
}
