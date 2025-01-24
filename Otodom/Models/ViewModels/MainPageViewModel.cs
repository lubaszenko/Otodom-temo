using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace Otodom.Models.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        public ICommand NavigateToLoginCommand { get; }
        public ICommand NavigateToRegisterCommand { get; }
        public ICommand NavigateToLoansCommand { get; }
        public ICommand NavigateToAdvertisementCommand { get; }
        public ICommand NavigateToAgenciesCommand { get; }
        public ICommand NavigateToAddAdvertisementCommand { get; }
        public ICommand NavigateToCurrenciesCommand { get; }

        public MainPageViewModel()
        {
            NavigateToLoginCommand = new AsyncRelayCommand(NavigateToLoginAsync);
            NavigateToRegisterCommand = new AsyncRelayCommand(NavigateToRegisterAsync);
            NavigateToLoansCommand = new AsyncRelayCommand(NavigateToLoansAsync);
            NavigateToAdvertisementCommand = new AsyncRelayCommand(NavigateToAdvertisementAsync);
            NavigateToAgenciesCommand = new AsyncRelayCommand(NavigateToAgenciesAsync);
            NavigateToAddAdvertisementCommand = new AsyncRelayCommand(NavigateToAddAdvertisementAsync);
            NavigateToCurrenciesCommand = new AsyncRelayCommand(NavigateToCurrenciesAsync);
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
            await Shell.Current.GoToAsync("//AddAdvertisement");
        }

        private async Task NavigateToCurrenciesAsync()
        {
            await Shell.Current.GoToAsync("//Currencies");
        }
    }
}
