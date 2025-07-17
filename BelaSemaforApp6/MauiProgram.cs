using BelaSemaforApp6.Models;
using BelaSemaforApp6.ViewModels;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;



namespace BelaSemaforApp6
{

#if ANDROID
    using Android.Graphics;
    using Android.Graphics.Drawables;
    using Microsoft.Maui.Controls.Handlers;
    using Microsoft.Maui.Platform;
#endif


    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .ConfigureMauiHandlers(handlers =>
                {
#if ANDROID
    handlers.AddHandler<RadioButton, RadioButtonHandler>();
#endif
                })

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

#if ANDROID
    RadioButtonHandler.Mapper.AppendToMapping("CustomRadio", (handler, view) =>
    {
        if (handler.PlatformView is Android.Widget.RadioButton nativeRadio)
        {
            var androidColor = Color.ParseColor(view.TextColor.ToArgbHex());

            nativeRadio.SetTextColor(androidColor);

            var buttonDrawable = nativeRadio.ButtonDrawable?.Mutate();
            if (buttonDrawable != null)
            {
                buttonDrawable.SetTint(androidColor);
                nativeRadio.SetButtonDrawable(buttonDrawable);
            }
        }
    });
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
