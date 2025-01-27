using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Otodom.Models;
using Otodom.Models.ViewModels;
using System.Linq;
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

        private readonly MainPageViewModel _mainPageViewModel;

        public ICommand LoginCommand { get; }
        public ICommand NavigateToRegisterCommand { get; }

        public LoginViewModel(AppDbContext dbContext, MainPageViewModel mainPageViewModel)
        {
            _dbContext = dbContext;
            _mainPageViewModel = mainPageViewModel;

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

            Console.WriteLine("Ustawiam IsLoggedIn na true");
            _mainPageViewModel.IsLoggedIn = true;
            _mainPageViewModel.IsNotLoggedIn = false;
            _mainPageViewModel.loggedUserId = user.Id;
            await App.Current.MainPage.DisplayAlert("Sukces", "Zalogowano pomyślnie", "OK");
            await Shell.Current.GoToAsync("//MainPage");
        }

        private async Task OnNavigateToRegisterAsync()
        {
            await Shell.Current.GoToAsync("//RegisterPage");
        }
    }
}
