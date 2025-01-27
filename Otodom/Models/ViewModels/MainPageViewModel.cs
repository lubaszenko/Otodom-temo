using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace Otodom.Models.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool isLoggedIn; 

        private bool _isNotLoggedIn;

        public int loggedUserId;

        public bool IsNotLoggedIn
        {
            get => _isNotLoggedIn;
            set => SetProperty(ref _isNotLoggedIn, value);
        }

        public ICommand NavigateToLoginCommand { get; }
        public ICommand NavigateToRegisterCommand { get; }
        public ICommand NavigateToLoansCommand { get; }
        public ICommand NavigateToAdvertisementCommand { get; }
        public ICommand NavigateToAgenciesCommand { get; }
        public ICommand NavigateToAddAdvertisementCommand { get; }
        public ICommand NavigateToCurrenciesCommand { get; }
        public ICommand LogoutCommand { get; }

        public MainPageViewModel()
        {
            NavigateToLoginCommand = new AsyncRelayCommand(NavigateToLoginAsync);
            NavigateToRegisterCommand = new AsyncRelayCommand(NavigateToRegisterAsync);
            NavigateToLoansCommand = new AsyncRelayCommand(NavigateToLoansAsync);
            NavigateToAdvertisementCommand = new AsyncRelayCommand(NavigateToAdvertisementAsync);
            NavigateToAgenciesCommand = new AsyncRelayCommand(NavigateToAgenciesAsync);
            NavigateToAddAdvertisementCommand = new AsyncRelayCommand(NavigateToAddAdvertisementAsync);
            NavigateToCurrenciesCommand = new AsyncRelayCommand(NavigateToCurrenciesAsync);
            LogoutCommand = new RelayCommand(Logout);

            IsLoggedIn = false;
            IsNotLoggedIn = true;
        }

        private async Task NavigateToLoginAsync()
        {
            await Shell.Current.GoToAsync("//LoginPage");  
        }

        private async Task NavigateToRegisterAsync()
        {
            await Shell.Current.GoToAsync("//RegisterPage");
        }

        private async Task NavigateToLoansAsync()
        {
            await Shell.Current.GoToAsync("//Loans");
        }

        private async Task NavigateToAdvertisementAsync()
        {
            await Shell.Current.GoToAsync("//Advertisement");
        }

        private async Task NavigateToAgenciesAsync()
        {
            await Shell.Current.GoToAsync("//Agencies");
        }

        private async Task NavigateToAddAdvertisementAsync()
        {
            if (IsLoggedIn)
            {
                await Shell.Current.GoToAsync("//AddAdvertisement");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Błąd", "Musisz być zalogowany, aby dodać ogłoszenie.", "OK");
            }
        }

        private async Task NavigateToCurrenciesAsync()
        {
            await Shell.Current.GoToAsync("//Currencies");
        }

        private void Logout()
        {
            IsLoggedIn = false;
            IsNotLoggedIn = true;
            loggedUserId = 0;
        }
    }
}
