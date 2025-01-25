using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using ApiConsumer;
using System.Collections.ObjectModel;
using ApiConsumer.DTO;

namespace Otodom.Models.ViewModels
{
    public partial class DisplayAdvertisementViewModel : ObservableObject
    {
        private readonly ApiClientService _clientService;

        public ICommand LoadDataHouseCommand { get; }

        public ICommand NavigateBackCommand { get; }

        public ICommand ShowDetailsCommand { get; }

        [ObservableProperty]
        private ObservableCollection<OgloszenieResponse> _ogloszenieznieruchomoscias = new();

        public DisplayAdvertisementViewModel(ApiClientService clientService) 
        {
            _clientService = clientService;
            ShowDetailsCommand = new Command<OgloszenieResponse>(ShowDetails);
            NavigateBackCommand = new AsyncRelayCommand(NavigateBackCommandAsync);
            LoadDataHouseCommand = new AsyncRelayCommand(LoadDataHouseAsync);
            LoadDataHouseCommand.Execute(null);
        }

        private async Task LoadDataHouseAsync()
        {
            try
            {
                Console.WriteLine("Endpoint LoadDataHouse works.");
                List<OgloszenieResponse> ogloszenies = await _clientService.GetOgloszenieznieruchomoscia();
                foreach(var Ogloszenie in ogloszenies)
                {
                    if(!_ogloszenieznieruchomoscias.Any(o => o.Id == Ogloszenie.Id))
                    {
                        _ogloszenieznieruchomoscias.Add(Ogloszenie);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Endpoint LoadDataHouse doesn't work.");
            }
        }

        private async void ShowDetails(OgloszenieResponse ogloszenie)
        {
            if (ogloszenie == null)
                return;

            string szczegoly = $"Imię wystawiającego: {ogloszenie.ImieKlienta}\n" +
                               $"Telefon: {ogloszenie.Telefon}\n" +
                               $"E-mail: { ogloszenie.Email}";

            await Application.Current.MainPage.DisplayAlert("Dane kontaktowe", szczegoly, "OK");
        }

        private async Task NavigateBackCommandAsync()
        {
            await Shell.Current.GoToAsync("//MainPage");
        }
        
    }
}
