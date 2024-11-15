using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BelaSemaforApp6.Models;

public class ColorsModel : INotifyPropertyChanged
{
    private Color _textColor = Color.Parse("#FFF8F8FF");
    private Color _backgroundColor = Color.Parse("#f0f0f0f0");
    private Color _headerColor = Color.Parse("#FF483D8B");

    public Color TextColor
    {
        get => _textColor;
        set
        {
            if (Equals(value, _textColor)) return;
            
            _textColor = value;
            OnPropertyChanged();
        }
    }

    public Color BackgroundColor
    {
        get => _backgroundColor;
        set
        {
            if (Equals(value, _backgroundColor)) return;
            
            _backgroundColor = value;
            OnPropertyChanged();
        }
    }

    public Color HeaderColor
    {
        get => _headerColor;
        set
        {
            if (Equals(value, _headerColor)) return;
            _headerColor = value;
            OnPropertyChanged();
        }
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string? propertyName = null) 
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}