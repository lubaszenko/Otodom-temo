using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ApiConsumer.IoC;
using Otodom.Models;
using Otodom.ViewModels;
using Otodom.Pages;
using Otodom.Models.ViewModels;

namespace Otodom
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            // Dodanie ApiClientService
            builder.Services.AddApiClientService(x => x.ApiBaseAddress = "http://10.0.2.2:5046/");

            // Ścieżka do bazy danych SQLite
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "AppDatabase.db");
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite($"Filename={dbPath}"));

            // Inicjalizacja bazy danych
            InitializeDatabase(dbPath);

            // Rejestracja ViewModels
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<RegisterViewModel>();
            builder.Services.AddTransient<MainPageViewModel>();
            builder.Services.AddTransient<AgenciesViewModel>();
            builder.Services.AddTransient<CurrenciesViewModel>();
            builder.Services.AddTransient<AddAdvertisementViewModel>();
            builder.Services.AddTransient<DisplayAdvertisementViewModel>();

            // Rejestracja Pages
            builder.Services.AddTransient<AddAdvertisement>();
            builder.Services.AddTransient<Advertisement>();
            builder.Services.AddTransient<Agencies>();
            builder.Services.AddTransient<Currencies>();
            builder.Services.AddTransient<Loans>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<RegisterPage>();

            return builder.Build();
        }

        // Metoda do inicjalizacji bazy danych
        private static void InitializeDatabase(string dbPath)
        {
            using var db = new AppDbContext(
                new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite($"Filename={dbPath}")
                .Options);

            // Tworzenie bazy danych i tabel, nawet jeśli istnieją
            db.Database.EnsureCreated();
        }
    }
}
