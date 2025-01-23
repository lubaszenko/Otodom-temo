using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Otodom.Models;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Otodom.ViewModels
{
    public partial class RegisterViewModel : ObservableObject
    {
        [ObservableProperty]
        private string login;

        [ObservableProperty]
        private string password;

        private readonly AppDbContext _dbContext;

        public ICommand RegisterCommand { get; }

        public RegisterViewModel(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            RegisterCommand = new AsyncRelayCommand(OnRegisterAsync);
        }

        private async Task OnRegisterAsync()
        {
            if (string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password))
            {
                await App.Current.MainPage.DisplayAlert("Błąd", "Wprowadź login i hasło", "OK");
                return;
            }

            var existingUser = _dbContext.Users.FirstOrDefault(u => u.Login == Login);
            if (existingUser != null)
            {
                await App.Current.MainPage.DisplayAlert("Błąd", "Użytkownik już istnieje", "OK");
                return;
            }

            _dbContext.Users.Add(new UserModel { Login = Login, Password = Password });
            await _dbContext.SaveChangesAsync();

            await App.Current.MainPage.DisplayAlert("Sukces", "Rejestracja zakończona pomyślnie", "OK");
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
