using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Configuration;

namespace BelaSemaforApp6.ViewModels;

partial class SettingsViewModel : ObservableObject
{
    public SettingsViewModel(IConfiguration config) {}

    [RelayCommand]
    private async void NavigateToGame()
    {
        await Shell.Current.GoToAsync("///GameView");
    }
}