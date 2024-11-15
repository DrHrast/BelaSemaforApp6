
using BelaSemaforApp6.ViewModels;

namespace BelaSemaforApp6.Views;

public partial class SettingsView : ContentPage
{
    public SettingsView()
    {
        InitializeComponent();
        
        BindingContext = IPlatformApplication.Current!.Services.GetService<SettingsViewModel>();
    }
}