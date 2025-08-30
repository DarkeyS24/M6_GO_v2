using Microsoft.Extensions.Logging;

namespace M6_GO_v2
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

            builder.Services.AddTransient<HttpClient>(http => 
            {
                return new HttpClient() { BaseAddress = new Uri("https://lhpmk91b-7100.brs.devtunnels.ms/api/") };
            });
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<IdosoPage>();
            builder.Services.AddSingleton<CuidadorPage>();
            builder.Services.AddSingleton<NovoAtendimentoPage>();

            return builder.Build();
        }
    }
}
