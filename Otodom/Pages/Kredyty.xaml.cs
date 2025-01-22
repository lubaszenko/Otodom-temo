using System;
using Microsoft.Maui.Controls;

namespace Otodom.Pages
{
    public partial class Kredyty : ContentPage
    {
        public int CenaNieruchomosci { get; set; } = 920000;
        public int WkladWlasny { get; set; } = 20; // W procentach
        public int OkresKredytowania { get; set; } = 30; // W latach
        public decimal OprocentowanieKredytu { get; set; } = 6.85m; // W procentach
        public decimal KwotaKredytuDoSplaty { get; set; }
        public decimal RataMiesieczna { get; set; }
        public decimal CalkowiteOdsetki { get; set; }

        public Kredyty()
        {
            InitializeComponent();
            BindingContext = this;
            ObliczKredyt();
        }

        private void ZmianaCenyNieruchomosci(object sender, ValueChangedEventArgs e)
        {
            CenaNieruchomosci = (int)e.NewValue;
            ObliczKredyt();
        }

        private void ZmianaWkladuWlasnego(object sender, ValueChangedEventArgs e)
        {
            WkladWlasny = (int)e.NewValue;
            ObliczKredyt();
        }

        private void ZmianaOkresuKredytowania(object sender, ValueChangedEventArgs e)
        {
            OkresKredytowania = (int)e.NewValue;
            ObliczKredyt();
        }

        private void ZmianaOprocentowania(object sender, ValueChangedEventArgs e)
        {
            OprocentowanieKredytu = (decimal)e.NewValue;
            ObliczKredyt();
        }

        private void ObliczKredyt()
        {
            decimal kwotaPozyczki = CenaNieruchomosci - (CenaNieruchomosci * WkladWlasny / 100m);
            decimal miesiecznaStopaProcentowa = OprocentowanieKredytu / 12 / 100;
            int iloscRat = OkresKredytowania * 12;

            RataMiesieczna = (kwotaPozyczki * miesiecznaStopaProcentowa) /
                             (1 - (decimal)Math.Pow(1 + (double)miesiecznaStopaProcentowa, -iloscRat));
            KwotaKredytuDoSplaty = RataMiesieczna * iloscRat;
            CalkowiteOdsetki = KwotaKredytuDoSplaty - kwotaPozyczki;

            OnPropertyChanged(nameof(RataMiesieczna));
            OnPropertyChanged(nameof(KwotaKredytuDoSplaty));
            OnPropertyChanged(nameof(CalkowiteOdsetki));
        }
    }
}
