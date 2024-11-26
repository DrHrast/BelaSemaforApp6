using System.Collections.ObjectModel;
using BelaSemaforApp6.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Configuration;

namespace BelaSemaforApp6.ViewModels;

partial class SettingsViewModel : ObservableObject
{
    [ObservableProperty] private AppSettingsModel _appSettings;
    [ObservableProperty] private bool _isHeader = true;
    [ObservableProperty] private bool _isText;
    [ObservableProperty] private bool _isBackground;
    [ObservableProperty] private string? _name;
    [ObservableProperty] private ObservableCollection<Color> _primaryColors =
    [
        Color.Parse("#fdfdfd"),
        Color.Parse("#696969"),
        Color.Parse("#181818"),
    ];
    [ObservableProperty] private ObservableCollection<Color> _secondaryColors =
    [
        Color.Parse("#000000"),
        Color.Parse("#FFFFFF"),
        Color.Parse("#D3D3D3"), // Light Gray
        Color.Parse("#708090"), // Slate Gray
        Color.Parse("#2F2F2F"), // Dark Gray
        Color.Parse("#D8BFD8"), // Thistle
        Color.Parse("#FF69B4"), // Hot Pink
        Color.Parse("#8A0303"), // Blood Red
        Color.Parse("#8B0000"), // Dark Red
        Color.Parse("#FF4500"), // Orange Red
        Color.Parse("#FFD700"), // Gold
        Color.Parse("#FFFF00"), // Yellow
        Color.Parse("#228B22"), // Forest Green
        Color.Parse("#2E8B57"), // Sea Green
        Color.Parse("#00CED1"), // Dark Turquoise
        Color.Parse("#1E90FF"), // Dodger Blue
        Color.Parse("#5D3FD3"), // Dark Purple
        Color.Parse("#4B0082") // Indigo
    ];

    [ObservableProperty] private ObservableCollection<int> _targetSores =
    [
            301,
            501,
            701,
            901,
            1001,
            1301
    ];
    
    // public ObservableCollection<PlayersModel> Players { get; set; } = new();
    // public ObservableCollection<TeamsModel> Teams { get; set; } = new();


    public SettingsViewModel(IConfiguration config, AppSettingsModel appSettingSettings)
    {
        AppSettings = appSettingSettings;
    }

    [RelayCommand]
    private async void NavigateToGame()
    {
        await Shell.Current.GoToAsync("///GameView");
    }
    
    [RelayCommand]
    private void SetPrimary(Color selectedColor)
    {
        AppSettings.PrimaryColor = selectedColor;
    }

    [RelayCommand]
    private void SetSecondary(Color selectedColor)
    {
        AppSettings.SecondaryColor = selectedColor;
    }

    [RelayCommand]
    private void SetTargetScore(int selectedScore)
    {
        AppSettings.TargetScore = selectedScore;
    }

}