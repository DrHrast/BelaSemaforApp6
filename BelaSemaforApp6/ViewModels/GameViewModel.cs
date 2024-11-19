using System.Collections.ObjectModel;
using BelaSemaforApp6.Models;
using BelaSemaforApp6.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Configuration;

namespace BelaSemaforApp6.ViewModels;

public partial class GameViewModel : ObservableObject
{
    [ObservableProperty] private ColorsModel _themeColors;
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
    [ObservableProperty] private bool _teamOneCallCheck = false;
    [ObservableProperty] private bool _teamTwoCallCheck = false;
    [ObservableProperty] private int _teamOneGameTotal = 0;
    [ObservableProperty] private int _teamTwoGameTotal = 0;
    [ObservableProperty] private ObservableCollection<TurnScoreModel>? _scores = [];

    private const int Maxscore = 162;
    private bool _canAddScore = true;

    public GameViewModel(IConfiguration config, ColorsModel colorSettings)
    {
        ThemeColors = colorSettings;
    }
    
    partial void OnTeamOneScoreChanged(int value)
    {
        if (_canAddScore == false) return;
        TeamTwoScore = Maxscore - value;
    }

    partial void OnTeamTwoScoreChanged(int value)
    {
        if (_canAddScore == false) return;
        TeamOneScore = Maxscore - value;
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
        var turnScoreModel = new TurnScoreModel {
            TeamOneScoreOnly = TeamOneScore, 
            IsTeamOneBelaChecked = TeamOneBela, 
            TeamOneCall = TeamOneCall,
            IsTeamOneCallChecked = TeamOneCallCheck,
            TeamTwoScoreOnly = TeamTwoScore,
            IsTeamTwoBelaChecked = TeamTwoBela,
            TeamTwoCall = TeamTwoCall,
            IsTeamTwoCallChecked = TeamTwoCallCheck
        };
        var turnScoreService = new TurnScoreService(turnScoreModel);
        Scores?.Add(turnScoreService.GetModel());
        TeamOneGameTotal += turnScoreService.GetModel().TeamOneTurnTotal;
        TeamTwoGameTotal += turnScoreService.GetModel().TeamTwoTurnTotal;
        ClearInputs();
    }

    private void ClearInputs()
    {
        _canAddScore = false;
        TeamOneCallCheck = false;
        TeamTwoCallCheck = false;
        TeamOneScore = 0;
        TeamTwoScore = 0;
        TeamOneCall = 0;
        TeamTwoCall = 0;
        TeamOneBela = false;
        TeamTwoBela = false;
        _canAddScore = true;
    }

    [RelayCommand]
    private async void NavigateToSettings()
    {
        await Shell.Current.GoToAsync($"///SettingsView");
    }
}
