using System;
using System.ComponentModel;
using Microsoft.Maui.Controls;

namespace Otodom.Pages
{
    public partial class Loans : ContentPage, INotifyPropertyChanged
    {
        private int _cenaNieruchomosci = 920000;
        public int CenaNieruchomosci
        {
            get => _cenaNieruchomosci;
            set
            {
                _cenaNieruchomosci = value;
                OnPropertyChanged();
                ObliczKredyt();
            }
        }

        private int _wkladWlasny = 20;
        public int WkladWlasny
        {
            get => _wkladWlasny;
            set
            {
                _wkladWlasny = value;
                OnPropertyChanged();
                ObliczKredyt();
            }
        }

        private int _okresKredytowania = 30;
        public int OkresKredytowania
        {
            get => _okresKredytowania;
            set
            {
                _okresKredytowania = value;
                OnPropertyChanged();
                ObliczKredyt();
            }
        }

        private decimal _oprocentowanieKredytu = 6.85m;
        public decimal OprocentowanieKredytu
        {
            get => _oprocentowanieKredytu;
            set
            {
                _oprocentowanieKredytu = value;
                OnPropertyChanged();
                ObliczKredyt();
            }
        }

        public decimal KwotaKredytuDoSplaty { get; private set; }
        public decimal RataMiesieczna { get; private set; }
        public decimal CalkowiteOdsetki { get; private set; }

        public Command NavigateBackCommand { get; }

        public Loans()
        {
            InitializeComponent();
            NavigateBackCommand = new Command(async () => await Shell.Current.GoToAsync("//MainPage"));
            BindingContext = this;
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
