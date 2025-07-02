using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BelaSemaforApp6.Models;

public class GameSettingsModel : INotifyPropertyChanged
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    private int _targetScore = 1001;
    private int _teamOneId;
    private int _teamTwoId;

    private TeamModel? _teamOne;
    private TeamModel? _teamTwo;

    public int TargetScore
    {
        get => _targetScore;
        set
        {
            if (_targetScore == value) return;
            _targetScore = value;
            OnPropertyChanged();
        }
    }

    public int TeamOneId
    {
        get => _teamOneId;
        set
        {
            if (_teamOneId == value) return;
            _teamOneId = value;
            OnPropertyChanged();
        }
    }

    public int TeamTwoId
    {
        get => _teamTwoId;
        set
        {
            if (_teamTwoId == value) return;
            _teamTwoId = value;
            OnPropertyChanged();
        }
    }

    [Ignore]
    public TeamModel? TeamOne
    {
        get => _teamOne;
        set
        {
            if (_teamOne == value) return;
            _teamOne = value;
            TeamOneId = value?.Id ?? 0;
            OnPropertyChanged();
        }
    }

    [Ignore]
    public TeamModel? TeamTwo
    {
        get => _teamTwo;
        set
        {
            if (_teamTwo == value) return;
            _teamTwo = value;
            TeamTwoId = value?.Id ?? 0;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
