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

        public CurrenciesViewModel()
        {
            GetLocationCommand = new AsyncRelayCommand(GetLocationAsync);
            NavigateBackCommand = new AsyncRelayCommand(NavigateBackCommandAsync);
        }

        private async Task GetLocationAsync()
        {
            try
            {
                // Sprawdzenie uprawnień
                var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                if (status != PermissionStatus.Granted)
                {
                    LocationMessage = "Brak dostępu do lokalizacji.";
                    return;
                }

                // Pobranie lokalizacji
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
                    LocationMessage = $"Szerokość: {location.Latitude}, Długość: {location.Longitude}";
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
        private async Task NavigateBackCommandAsync()
        {
            await Shell.Current.GoToAsync("//MainPage");
        }
    }
}
