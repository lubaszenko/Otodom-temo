using System;
using System.Collections.Generic;

namespace Otodom.Models
{
    public partial class Ogloszenie
    {
        public int IdOgloszenia { get; set; }
        public string Tytul { get; set; } = null!;
        public DateTime DataDodania { get; set; }
        public bool Status { get; set; }
        public string Opis { get; set; } = null!;
        public decimal Cena { get; set; }
        public int KlientIdKlienta { get; set; }
        public int NieruchomoscIdNieruchomosci { get; set; }

        public virtual Klient KlientIdKlientaNavigation { get; set; } = null!;
        public virtual Nieruchomosc NieruchomoscIdNieruchomosciNavigation { get; set; } = null!;
    }
}
