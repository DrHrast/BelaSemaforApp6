using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Configuration;

namespace BelaSemaforApp6.ViewModels;

partial class SettingsViewModel : ObservableObject
{
    private Color _selectedColor;
    public Color SelectedColor
    {
        get => _selectedColor;
        set
        {
            if (_selectedColor != value)
            {
                _selectedColor = value;
                OnPropertyChanged();
                ApplyThemeColor(_selectedColor); // Apply the selected color to the theme
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
    
    private void ApplyThemeColor(Color selectedColor)
    {
        // Apply the selected color to your app's theme
        Console.WriteLine($"Selected Color: {selectedColor}");
        // Example: Update your application's global resources, styles, or specific elements
    }
}