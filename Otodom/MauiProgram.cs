using Microsoft.Extensions.Logging;
using ApiConsumer.IoC;

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
            return builder.Build();
        }
    }
}
