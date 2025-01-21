using System;
using System.Collections.Generic;

namespace Otodom.Models
{
    public partial class Zdjecie
    {
        public int IdZdjecia { get; set; }
        public string ZdjecieData { get; set; }
        public int NieruchomoscIdNieruchomosci { get; set; }

        public virtual Nieruchomosc NieruchomoscIdNieruchomosciNavigation { get; set; } = null!;
    }
}
