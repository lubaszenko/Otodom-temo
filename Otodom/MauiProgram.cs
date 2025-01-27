using Otodom.Models;
using Otodom.ViewModels;
using Otodom.Pages;
using Otodom.Models.ViewModels;
using ApiConsumer.IoC;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

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

            builder.Services.AddApiClientService(x => x.ApiBaseAddress = "http://10.0.2.2:5046/");

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "AppDatabase.db");
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite($"Filename={dbPath}"));

            InitializeDatabase(dbPath);

            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<RegisterViewModel>();
            builder.Services.AddTransient<AgenciesViewModel>();
            builder.Services.AddTransient<CurrenciesViewModel>();
            builder.Services.AddSingleton<AddAdvertisementViewModel>();
            builder.Services.AddSingleton<DisplayAdvertisementViewModel>();

            builder.Services.AddSingleton<AddAdvertisement>();
            builder.Services.AddSingleton<Advertisement>();
            builder.Services.AddTransient<Agencies>();
            builder.Services.AddTransient<Currencies>();
            builder.Services.AddTransient<Loans>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<RegisterPage>();

            return builder.Build();
        }

        private static void InitializeDatabase(string dbPath)
        {
            using var db = new AppDbContext(
                new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite($"Filename={dbPath}")
                .Options);

            db.Database.EnsureCreated();
        }
    }
}
