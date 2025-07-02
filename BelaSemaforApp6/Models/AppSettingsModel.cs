using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BelaSemaforApp6.Models;

public class AppSettingsModel : INotifyPropertyChanged
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    private string _primaryColorHex = "#FFF8F8FF";
    private string _secondaryColorHex = "#FF483D8B";

    [Ignore]
    public Color PrimaryColor
    {
        get => Color.FromArgb(_primaryColorHex);
        set
        {
            if (_primaryColorHex == value.ToArgbHex()) return;
            _primaryColorHex = value.ToArgbHex();
            OnPropertyChanged();
            OnPropertyChanged(nameof(PrimaryColor));
        }
    }

    [Ignore]
    public Color SecondaryColor
    {
        get => Color.FromArgb(_secondaryColorHex);
        set
        {
            if (_secondaryColorHex == value.ToArgbHex()) return;
            _secondaryColorHex = value.ToArgbHex();
            OnPropertyChanged();
            OnPropertyChanged(nameof(SecondaryColor));
        }
    }

    public string PrimaryColorHex
    {
        get => _primaryColorHex;
        set
        {
            if (_primaryColorHex == value) return;
            _primaryColorHex = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(PrimaryColor));
        }
    }

    public string SecondaryColorHex
    {
        get => _secondaryColorHex;
        set
        {
            if (_secondaryColorHex == value) return;
            _secondaryColorHex = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(SecondaryColor));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

public static class ColorExtensions
{
    public static string ToArgbHex(this Color color)
    {
        return color.ToHex(); 
    }
}
