using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Microsoft.Maui.Platform;

namespace BelaSemaforApp6
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetStatusBarColor(Color.FromArgb("#BBBBBBBB"));
        }

        public static void SetStatusBarColor(Microsoft.Maui.Graphics.Color mauiColor)
        {
            var activity = Platform.CurrentActivity;
            if (activity == null || activity.Window == null)
                return;

            var androidColor = mauiColor.ToPlatform();
            activity.Window.SetStatusBarColor(androidColor);

            // Optional: change icon color to dark for light backgrounds
            activity.Window.DecorView.SystemUiVisibility = (StatusBarVisibility)SystemUiFlags.LightStatusBar;
        }
    }
}
