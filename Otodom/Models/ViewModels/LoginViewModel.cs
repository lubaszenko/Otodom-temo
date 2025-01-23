using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Otodom.Models;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Otodom.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty]
        private string login;

        [ObservableProperty]
        private string password;

        private readonly AppDbContext _dbContext;

        public ICommand LoginCommand { get; }
        public ICommand NavigateToRegisterCommand { get; }

        public LoginViewModel(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            LoginCommand = new AsyncRelayCommand(OnLoginAsync);
            NavigateToRegisterCommand = new AsyncRelayCommand(OnNavigateToRegisterAsync);
        }

        private async Task OnLoginAsync()
        {
            if (string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password))
            {
                await App.Current.MainPage.DisplayAlert("Błąd", "Podaj login i hasło", "OK");
                return;
            }

            var user = _dbContext.Users.FirstOrDefault(u => u.Login == Login && u.Password == Password);
            if (user == null)
            {
                await App.Current.MainPage.DisplayAlert("Błąd", "Nieprawidłowe dane logowania", "OK");
                return;
            }

            await App.Current.MainPage.DisplayAlert("Sukces", "Zalogowano pomyślnie", "OK");
            await Shell.Current.GoToAsync("//MainPage");
        }

        private async Task OnNavigateToRegisterAsync()
        {
            await Shell.Current.GoToAsync("//RegisterPage");
        }
    }
}
