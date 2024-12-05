using System.Collections.ObjectModel;
using BelaSemaforApp6.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Configuration;

namespace BelaSemaforApp6.ViewModels;

public partial class GameViewModel : ObservableObject
{
    [ObservableProperty] private AppSettingsModel _appSettings;
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
    [ObservableProperty] private bool _teamOneCallCheck;
    [ObservableProperty] private bool _teamTwoCallCheck;
    [ObservableProperty] private int _teamOneGameTotal;
    [ObservableProperty] private int _teamTwoGameTotal;
    [ObservableProperty] private bool _isStilja;
    [ObservableProperty] private bool _hasTeamOneStilja;
    [ObservableProperty] private bool _hasTeamTwoStilja;
    [ObservableProperty] private bool _teamOneStilja;
    [ObservableProperty] private bool _teamTwoStilja;
    [ObservableProperty] private ObservableCollection<GameScoreModel>? _scores = [];

    private const int Maxscore = 162;
    private bool _canAddScore = true;

    public GameViewModel(IConfiguration config, AppSettingsModel appSettingSettings)
    {
        AppSettings = appSettingSettings;
    }
    
    partial void OnTeamOneScoreChanged(int value)
    {
        ClearStiljaComponents();
        if (_canAddScore == false) return;
        TeamTwoScore = Maxscore - value;
        if (TeamOneScore == 0)
        {
            IsStilja = true;
            HasTeamTwoStilja = true;
            TeamTwoScore = Maxscore;
        }
    }

    partial void OnTeamTwoScoreChanged(int value)
    {
        ClearStiljaComponents();
        if (_canAddScore == false) return;
        TeamOneScore = Maxscore - value;
        if (TeamTwoScore == 0)
        {
            IsStilja = true;
            HasTeamOneStilja = true;
            TeamOneScore = Maxscore;
        }
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
        var teamOneTurnScoreModel = new ScoreModel {
            ScoreOnly = TeamOneScore, 
            Bela = TeamOneBela ? 20 : 0, 
            Call = TeamOneCall,
            IsCallChecked = TeamOneCallCheck,
            IsStilja = TeamOneStilja
        };
        var teamTwoTurnScoreModel = new ScoreModel
        {
            ScoreOnly = TeamTwoScore,
            Bela = TeamTwoBela ? 20 : 0,
            Call = TeamTwoCall,
            IsCallChecked = TeamTwoCallCheck,
            IsStilja = TeamTwoStilja
        };
        var gameScoreModel = new GameScoreModel(teamOneTurnScoreModel, teamTwoTurnScoreModel);
        Scores?.Add(gameScoreModel);
        TeamOneGameTotal += gameScoreModel.TeamOneScore;
        TeamTwoGameTotal += gameScoreModel.TeamTwoScore;
        CheckForWin(gameScoreModel);
        ClearInputs();
    }

    private void CheckForWin(GameScoreModel gameScoreModel)
    {
        if (TeamOneGameTotal >= gameScoreModel.TargetScore)
        {
            App.Current.MainPage.DisplayAlert("Pobjeda", $"Team {TeamOneName} won!!", "Nova Igra");
            NewGame();
        }

        if (TeamTwoGameTotal >= gameScoreModel.TargetScore)
        {
            App.Current.MainPage.DisplayAlert("Pobjeda", $"Team {TeamTwoName} won!!", "Nova Igra");
            NewGame();
        }
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
        ClearStiljaComponents();
        _canAddScore = true;
    }

    private void ClearStiljaComponents()
    {
        HasTeamOneStilja = false;
        HasTeamTwoStilja = false;
        TeamOneStilja = false;
        TeamTwoStilja = false;
        IsStilja = false;
    }

    private void NewGame()
    {
        ClearInputs();
        Scores.Clear();
        TeamOneGameTotal = 0;
        TeamTwoGameTotal = 0;
    }

    [RelayCommand]
    private async void NavigateToSettings()
    {
        await Shell.Current.GoToAsync($"///SettingsView");
    }
}
