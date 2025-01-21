using System;
using System.Collections.Generic;

namespace Otodom.Models
{
    public partial class Klient
    {
        public Klient()
        {
            Ogloszenies = new HashSet<Ogloszenie>();
        }

        public int IdKlienta { get; set; }
        public string Imie { get; set; } = null!;
        public string Nazwisko { get; set; } = null!;
        public string Email { get; set; } = null!;
        public decimal NrTelefonuKlienta { get; set; }
        public int? AgencjaIdAgencji { get; set; }

        public virtual Agencja? AgencjaIdAgencjiNavigation { get; set; }
        public virtual ICollection<Ogloszenie> Ogloszenies { get; set; }
    }
}
