using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BelaSemaforApp6.Models;

public class GameSettingsModel : INotifyPropertyChanged
{
    public int ID { get; set; }
    private int _targetScore = 1001;
    private TeamModel? _teamOne = new TeamModel
    {
        Name = "TeamOne"
    };
    private TeamModel? _teamTwo = new TeamModel
    {
        Name = "TeamTwo"
    };

    public int TargetScore
    {
        get => _targetScore;
        set
        {
            if (Equals(value, _targetScore)) return;
            
            _targetScore = value;
            OnPropertyChanged();
        }
    }

    public TeamModel TeamOne
    {
        get => _teamOne;
        set
        {
            if (Equals(value, _teamOne)) return;
            
            _teamOne = value;
            OnPropertyChanged();
        }
    }

    public TeamModel TeamTwo
    {
        get => _teamTwo;
        set
        {
            if (Equals(value, _teamTwo)) return;
            
            _teamTwo = value;
            OnPropertyChanged();
        }
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string? propertyName = null) 
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}