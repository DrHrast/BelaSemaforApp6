﻿using System.Collections.ObjectModel;
using BelaSemaforApp6.Models;
using BelaSemaforApp6.Views;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Configuration;

namespace BelaSemaforApp6.ViewModels;

public partial class GameViewModel : ObservableObject
{
    [ObservableProperty] private AppSettingsModel _appSettings;
    [ObservableProperty] private GameSettingsModel _gameSettings;
    [ObservableProperty] private string? _teamOneName = "TeamOne";
    [ObservableProperty] private string? _teamTwoName = "TeamTwo";
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
    [ObservableProperty] private string _teamOneScoreInput = "0";
    [ObservableProperty] private string _teamTwoScoreInput = "0";
    [ObservableProperty] private ObservableCollection<GameScoreModel>? _scores = [];

    private int _teamOneScore;
    private int _teamTwoScore;
    private const int Maxscore = 162;
    private bool _canAddScore = true;

    public GameViewModel(IConfiguration config, AppSettingsModel appSettingSettings, GameSettingsModel gameSettings)
    {
        AppSettings = appSettingSettings;
        GameSettings = gameSettings;
    }

    partial void OnTeamOneScoreInputChanged(string value)
    {
        ClearStiljaComponents();

        if (!_canAddScore) return;

        if (string.IsNullOrWhiteSpace(value) || !int.TryParse(value, out var parsed) || parsed > Maxscore)
        {
            TeamOneScoreInput = "0";
            TeamTwoScoreInput = Maxscore.ToString();

            return;
        }

        TeamOneScoreInput = parsed.ToString();
        TeamTwoScoreInput = (Maxscore - parsed).ToString();

        if (parsed == 0)
        {
            IsStilja = true;
            HasTeamTwoStilja = true;
            TeamTwoScoreInput = Maxscore.ToString();
        }
    }

    partial void OnTeamTwoScoreInputChanged(string value)
    {
        ClearStiljaComponents();

        if (!_canAddScore) return;

        if (string.IsNullOrWhiteSpace(value) || !int.TryParse(value, out var parsed) || parsed > Maxscore)
        {
            TeamTwoScoreInput = 0.ToString();
            TeamOneScoreInput = Maxscore.ToString();

            return;
        }

        TeamTwoScoreInput = parsed.ToString();
        TeamOneScoreInput = (Maxscore - parsed).ToString();

        if (parsed == 0)
        {
            IsStilja = true;
            HasTeamOneStilja = true;
            TeamOneScoreInput = Maxscore.ToString();
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
        if (IsInputValid())
        {
            var teamOneTurnScoreModel = new ScoreModel {
                ScoreOnly = _teamOneScore, 
                Bela = TeamOneBela ? 20 : 0, 
                Call = TeamOneCall,
                IsCallChecked = TeamOneCallCheck,
                IsStilja = TeamOneStilja
            };
            var teamTwoTurnScoreModel = new ScoreModel
            {
                ScoreOnly = _teamTwoScore,
                Bela = TeamTwoBela ? 20 : 0,
                Call = TeamTwoCall,
                IsCallChecked = TeamTwoCallCheck,
                IsStilja = TeamTwoStilja
            };
            var gameScoreModel = new GameScoreModel(teamOneTurnScoreModel, teamTwoTurnScoreModel);
            gameScoreModel.CalculateScore();
            //Scores?.Add(gameScoreModel);
            Scores?.Insert(0, gameScoreModel);
            TeamOneGameTotal += gameScoreModel.TeamOneScore;
            TeamTwoGameTotal += gameScoreModel.TeamTwoScore;
            CheckForWin();
        }
        ClearInputs();
    }

    private void CheckForWin()
    {
        if (TeamOneGameTotal >= GameSettings.TargetScore)
        {
            App.Current?.MainPage?.DisplayAlert("Pobjeda", $"Team {TeamOneName} won!!", "Nova Igra");
            NewGame();
        }

        if (TeamTwoGameTotal >= GameSettings.TargetScore)
        {
            App.Current?.MainPage?.DisplayAlert("Pobjeda", $"Team {TeamTwoName} won!!", "Nova Igra");
            NewGame();
        }
    }

    private void ClearInputs()
    {
        _canAddScore = false;
        TeamOneCallCheck = false;
        TeamTwoCallCheck = false;
        _teamOneScore = 0;
        TeamOneScoreInput = "0";
        _teamTwoScore = 0;
        TeamTwoScoreInput = "0";
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

    private bool IsInputValid()
    {
        _teamOneScore = int.Parse(TeamOneScoreInput);
        _teamTwoScore = int.Parse(TeamTwoScoreInput);
        if (_teamOneScore != 0 || _teamTwoScore != 0)
        {
            return true;
        }

        return false;
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
    
    [RelayCommand]
    private async void NavigateToHelp()
    {
        var popup = new HelpPopup();
        await App.Current.MainPage.ShowPopupAsync(popup);
    }
}
