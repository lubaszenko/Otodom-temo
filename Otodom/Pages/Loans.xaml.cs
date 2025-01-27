using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace Otodom.Pages
{
    public partial class Loans : ContentPage, INotifyPropertyChanged
    {
        private readonly HttpClient _httpClient = new HttpClient();

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

        private string _selectedCurrency = "PLN";
        public string SelectedCurrency
        {
            get => _selectedCurrency;
            set
            {
                _selectedCurrency = value;
                OnPropertyChanged();
                ObliczPrzewalutowanie();
            }
        }

        public ObservableCollection<string> Currencies { get; } = new ObservableCollection<string> { "PLN", "EUR", "USD" };

        public decimal RataMiesieczna { get; private set; } 
        public decimal KwotaKredytuDoSplaty { get; private set; } 
        public decimal CalkowiteOdsetki { get; private set; } 

        public string PrzeliczonaRataMiesieczna { get; private set; }
        public string PrzeliczonaKwotaKredytuDoSplaty { get; private set; }
        public string PrzeliczoneCalkowiteOdsetki { get; private set; }

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

            PrzeliczonaRataMiesieczna = $"{RataMiesieczna:N2} PLN";
            PrzeliczonaKwotaKredytuDoSplaty = $"{KwotaKredytuDoSplaty:N2} PLN";
            PrzeliczoneCalkowiteOdsetki = $"{CalkowiteOdsetki:N2} PLN";

            OnPropertyChanged(nameof(RataMiesieczna));
            OnPropertyChanged(nameof(KwotaKredytuDoSplaty));
            OnPropertyChanged(nameof(CalkowiteOdsetki));

            OnPropertyChanged(nameof(PrzeliczonaRataMiesieczna));
            OnPropertyChanged(nameof(PrzeliczonaKwotaKredytuDoSplaty));
            OnPropertyChanged(nameof(PrzeliczoneCalkowiteOdsetki));
        }

        private async void ObliczPrzewalutowanie()
        {
            try
            {
                if (SelectedCurrency == "PLN")
                {
                    PrzeliczonaRataMiesieczna = $"{RataMiesieczna:N2} PLN";
                    PrzeliczonaKwotaKredytuDoSplaty = $"{KwotaKredytuDoSplaty:N2} PLN";
                    PrzeliczoneCalkowiteOdsetki = $"{CalkowiteOdsetki:N2} PLN";
                }
                else
                {
                    decimal exchangeRate = await GetExchangeRate(SelectedCurrency);

                    PrzeliczonaRataMiesieczna = $"{(RataMiesieczna / exchangeRate):N2} {SelectedCurrency}";
                    PrzeliczonaKwotaKredytuDoSplaty = $"{(KwotaKredytuDoSplaty / exchangeRate):N2} {SelectedCurrency}";
                    PrzeliczoneCalkowiteOdsetki = $"{(CalkowiteOdsetki / exchangeRate):N2} {SelectedCurrency}";
                }

                OnPropertyChanged(nameof(PrzeliczonaRataMiesieczna));
                OnPropertyChanged(nameof(PrzeliczonaKwotaKredytuDoSplaty));
                OnPropertyChanged(nameof(PrzeliczoneCalkowiteOdsetki));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"B³¹d podczas przeliczania walut: {ex.Message}");
            }
        }

        private async Task<decimal> GetExchangeRate(string currency)
        {
            if (currency == "PLN")
                return 1m;

            var response = await _httpClient.GetStringAsync($"https://api.nbp.pl/api/exchangerates/rates/a/{currency.ToLower()}/?format=json");
            var json = JsonDocument.Parse(response);
            return json.RootElement.GetProperty("rates")[0].GetProperty("mid").GetDecimal();
        }
    }
}
