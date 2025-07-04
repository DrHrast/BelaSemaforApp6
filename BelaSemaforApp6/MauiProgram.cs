﻿using BelaSemaforApp6.Models;
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
                .RegisterServices()
                .RegisterViews()
                .RegisterViewModels()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
        
        private static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
        {
            return builder;
        }

        private static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<GameViewModel>();
            builder.Services.AddTransient<SettingsViewModel>();
            builder.Services.AddTransient<HelpPopupViewModel>();
            return builder;
        }
        
        private static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<AppSettingsModel>();
            builder.Services.AddSingleton<GameSettingsModel>();
            builder.Services.AddSingleton<DatabaseManager>();
            return builder;
        }
    }
    
}
