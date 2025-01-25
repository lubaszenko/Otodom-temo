using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace Otodom.Models.ViewModels
{
    public partial class CurrenciesViewModel : ObservableObject
    {

        [ObservableProperty]
        private string locationMessage;
        public ICommand GetLocationCommand { get; }
        public ICommand NavigateBackCommand { get; }
        public ICommand OpenMapCommand { get; }

        private double latitude; 
        private double longitude; 

        public CurrenciesViewModel()
        {
            GetLocationCommand = new AsyncRelayCommand(GetLocationAsync);
            NavigateBackCommand = new AsyncRelayCommand(NavigateBackCommandAsync);
            OpenMapCommand = new AsyncRelayCommand(OpenLocationInGoogleMapsAsync);
        }

        private async Task GetLocationAsync()
        {
            try
            {
                var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                if (status != PermissionStatus.Granted)
                {
                    LocationMessage = "Brak dostępu do lokalizacji.";
                    return;
                }

                var location = await Geolocation.Default.GetLastKnownLocationAsync();
                if (location == null)
                {
                    location = await Geolocation.Default.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Medium,
                        Timeout = TimeSpan.FromSeconds(30)
                    });
                }

                if (location != null)
                {
                    latitude = location.Latitude;
                    longitude = location.Longitude;
                    LocationMessage = $"Szerokość: {latitude}, Długość: {longitude}";
                }
                else
                {
                    LocationMessage = "Nie udało się pobrać lokalizacji.";
                }
            }
            catch (FeatureNotSupportedException)
            {
                LocationMessage = "GPS nie jest wspierany na tym urządzeniu.";
            }
            catch (PermissionException)
            {
                LocationMessage = "Brak odpowiednich uprawnień do lokalizacji.";
            }
            catch (Exception ex)
            {
                LocationMessage = $"Błąd: {ex.Message}";
            }
        }

        private async Task OpenLocationInGoogleMapsAsync()
        {
            if (latitude == 0 || longitude == 0)
            {
                LocationMessage = "Najpierw pobierz lokalizację.";
                return;
            }

            var url = $"https://www.google.com/maps?q={latitude},{longitude}";
            await Launcher.Default.OpenAsync(url);
        }

        private async Task NavigateBackCommandAsync()
        {
            await Shell.Current.GoToAsync("//MainPage");
        }
    }
}
