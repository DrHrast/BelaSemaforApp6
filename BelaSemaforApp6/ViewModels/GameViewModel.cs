using System.Collections.ObjectModel;
using BelaSemaforApp6.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Configuration;

namespace BelaSemaforApp6.ViewModels;

public partial class GameViewModel : ObservableObject
{
    
    [ObservableProperty] private string? _teamOneName = "TeamOne";
    [ObservableProperty] private string? _teamTwoName = "TeamTwo";
    [ObservableProperty] private int _teamOneScore;
    [ObservableProperty] private int _teamTwoScore;
    [ObservableProperty] private int _teamOneCall;
    [ObservableProperty] private int _teamTwoCall;
    [ObservableProperty] private bool _teamOneBela;
    [ObservableProperty] private bool _teamTwoBela;
    [ObservableProperty] private int _teamOneTurnScore;
    [ObservableProperty] private int _teamTwoTurnScore;
    [ObservableProperty] private ObservableCollection<TurnScore>? _scores = [];

    private int maxScore = 162;
    
    public GameViewModel(IConfiguration config) {}
    
    partial void OnTeamOneScoreChanged(int value)
    {
        TeamTwoScore = maxScore - value;
    }

    partial void OnTeamTwoScoreChanged(int value)
    {
        TeamOneScore = maxScore - value;
    }

    partial void OnTeamOneBelaChanged(bool value)
    {
        if (value) TeamTwoBela = !value;
    }

    partial void OnTeamTwoBelaChanged(bool value)
    {
        if (value) TeamOneBela = !value;
    }

    [RelayCommand]
    private void AddScore()
    {
        Scores.Add(new TurnScore
        {
            TeamOne = TeamOneScore, 
            OneBela = TeamOneBela, 
            OneCall = TeamOneCall,
            TeamTwo = TeamTwoScore,
            TwoBela = TeamTwoBela,
            TwoCall = TeamTwoCall
        });
    }
}
