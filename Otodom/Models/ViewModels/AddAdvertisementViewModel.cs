using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using ApiConsumer;
using System.Collections.ObjectModel;
using ApiConsumer.DTO;
using System.ComponentModel;

namespace Otodom.Models.ViewModels
{
    public partial class AddAdvertisementViewModel : ObservableObject
    {
        private readonly ApiClientService _clientService;

        private readonly MainPageViewModel _mainPageViewModel;

        private readonly DisplayAdvertisementViewModel _displayAdvertisementViewModel;

        public ICommand AddAdvertisement {  get; }

        public ICommand NavigateBackCommand { get; }

        [ObservableProperty]
        private OgloszenieRequest _advertisementRequest;

        public AddAdvertisementViewModel(ApiClientService clientService, MainPageViewModel mainPageViewModel, DisplayAdvertisementViewModel displayAdvertisementViewModel)
        {
            NavigateBackCommand = new AsyncRelayCommand(NavigateBackCommandAsync);
            _clientService = clientService;
            _advertisementRequest = new OgloszenieRequest();
            AddAdvertisement = new AsyncRelayCommand(AddAdvertisementAsync);
            _displayAdvertisementViewModel = displayAdvertisementViewModel;
            _mainPageViewModel = mainPageViewModel;
        }

        public async Task AddAdvertisementAsync()
        {
            try
            {
                AdvertisementRequest.Status = true;
                AdvertisementRequest.DataDodania = DateTime.Now;
                AdvertisementRequest.KlientIdKlienta = _mainPageViewModel.loggedUserId;
                await _clientService.PostOgloszenieZnieruchomoscia(AdvertisementRequest);
                AdvertisementRequest = new OgloszenieRequest();
                _displayAdvertisementViewModel.LoadDataHouseCommand.Execute(null);
                await App.Current.MainPage.DisplayAlert("Sukces", "Ogłoszenie dodano pomyślnie.", "OK");
                await Shell.Current.GoToAsync("//MainPage");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Błąd", "Podano błędne dane.", "OK");
            }
        }

        private async Task NavigateBackCommandAsync()
        {
            await Shell.Current.GoToAsync("//MainPage");
        }
    }
}
