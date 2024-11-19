using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BelaSemaforApp6.Models;

public class ColorsModel : INotifyPropertyChanged
{
    private Color _primaryColor = Color.Parse("#FFF8F8FF");
    private Color _secondaryColor = Color.Parse("#FF483D8B");

    public Color PrimaryColor
    {
        get => _primaryColor;
        set
        {
            if (Equals(value, _primaryColor)) return;
            
            _primaryColor = value;
            OnPropertyChanged();
        }
    }

    public Color SecondaryColor
    {
        get => _secondaryColor;
        set
        {
            if (Equals(value, _secondaryColor)) return;
            _secondaryColor = value;
            OnPropertyChanged();
        }
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string? propertyName = null) 
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}