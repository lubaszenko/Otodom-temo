namespace ApiConsumer.DTO
{
    public class AgencjaRequest
    {
        public string NazwaAgencji { get; set; } = null!;
        public decimal NrTelefonuAgencji { get; set; }
        public string Email { get; set; } = null!;
        public decimal Nip { get; set; }
    }
}
