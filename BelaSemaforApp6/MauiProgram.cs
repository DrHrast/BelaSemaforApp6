using BelaSemaforApp6.Services;
using BelaSemaforApp6.ViewModels;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace BelaSemaforApp6
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
            
            // ViewModels
            builder.Services.AddTransient<GameViewModel>();
            builder.Services.AddTransient<SettingsViewModel>();
            
            // Services
            builder.Services.AddTransient<TurnScoreService>();
#endif

            return builder.Build();
        }
    }
}
