using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Otodom.Models.ViewModels
{
    public partial class WyswietlanieOgloszeniaViewModel : ObservableObject
    {
        public ICommand NavigateToLoginCommand { get; }
        public ICommand NavigateToRegisterCommand { get; }
        public ICommand NavigateToKredytyCommand { get; }

        public WyswietlanieOgloszeniaViewModel()
        {
            NavigateToLoginCommand = new AsyncRelayCommand(NavigateToLoginAsync);
            NavigateToRegisterCommand = new AsyncRelayCommand(NavigateToRegisterAsync);
            NavigateToKredytyCommand = new AsyncRelayCommand(NavigateToKredytyAsync);
        }

        private async Task NavigateToLoginAsync()
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }

        private async Task NavigateToRegisterAsync()
        {
            await Shell.Current.GoToAsync("//RegisterPage");
        }

        private async Task NavigateToKredytyAsync()
        {
            await Shell.Current.GoToAsync("//KredytyPage");
        }
    }
}
