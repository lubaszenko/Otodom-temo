using System;
using System.Collections.Generic;

namespace Otodom.Models
{
    public partial class Agencja
    {
        public Agencja()
        {
            Klients = new HashSet<Klient>();
        }

        public int IdAgencji { get; set; }
        public string NazwaAgencji { get; set; } = null!;
        public decimal NrTelefonuAgencji { get; set; }
        public string Email { get; set; } = null!;
        public decimal Nip { get; set; }

        public virtual ICollection<Klient> Klients { get; set; }
    }
}
