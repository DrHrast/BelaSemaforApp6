using System.Collections.ObjectModel;
using BelaSemaforApp6.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Configuration;

namespace BelaSemaforApp6.ViewModels;

partial class SettingsViewModel : ObservableObject
{
    private Color _selectedColor;
    public ColorsModel ThemeColors { get; } = new ColorsModel();
    public Color SelectedColor
    {
        get => _selectedColor;
        set
        {
            if (_selectedColor != value)
            {
                _selectedColor = value;
                OnPropertyChanged(nameof(SelectedColor));
                UpdateThemeColors(_selectedColor);
            }
        }
    }
    
    [ObservableProperty] private ObservableCollection<Color> _colors = new ObservableCollection<Color>
    {
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
        Color.Parse("#4B0082"), // Indigo
    };
    
    public SettingsViewModel(IConfiguration config) {}

    [RelayCommand]
    private async void NavigateToGame()
    {
        await Shell.Current.GoToAsync("///GameView");
    }
    
    private void UpdateThemeColors(Color selectedColor)
    {
        // Update specific theme colors (adjust logic as needed)
        ThemeColors.TextColor = selectedColor;
        ThemeColors.BackgroundColor = selectedColor.WithLuminosity((float)0.9); // Slightly lighter for background
        ThemeColors.HeaderColor = selectedColor.WithLuminosity((float)0.7); // Medium lightness for headers
    }
}