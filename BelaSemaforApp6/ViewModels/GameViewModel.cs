using System.Collections.ObjectModel;
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

    [ObservableProperty] private string? _teamOneName;
    [ObservableProperty] private string? _teamTwoName;

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
    [ObservableProperty] private ObservableCollection<GameScoreModel> _scores = new();

    private int _teamOneScore;
    private int _teamTwoScore;
    private const int Maxscore = 162;
    private bool _canAddScore = true;
    private readonly DatabaseManager _db;

    public GameViewModel(AppSettingsModel appSettings, GameSettingsModel gameSettings, DatabaseManager db)
    {
        _db = db;
        AppSettings = appSettings;
        GameSettings = gameSettings;

        var savedApp = _db.GetAppSettings();
        if (savedApp != null)
        {
            AppSettings.PrimaryColor = savedApp.PrimaryColor;
            AppSettings.SecondaryColor = savedApp.SecondaryColor;
        }

        var savedGame = _db.GetGameSettings();
        if (savedGame != null)
        {
            GameSettings.TargetScore = savedGame.TargetScore;
            GameSettings.TeamOneId = savedGame.TeamOneId;
            GameSettings.TeamTwoId = savedGame.TeamTwoId;
            GameSettings.TeamOne = _db.GetTeamById(savedGame.TeamOneId);
            GameSettings.TeamTwo = _db.GetTeamById(savedGame.TeamTwoId);
        }

        TeamOneName = GameSettings.TeamOne?.Name ?? "TeamOne";
        TeamTwoName = GameSettings.TeamTwo?.Name ?? "TeamTwo";

        var savedScores = _db.GetGameScores();

        for (int i = 0; i < savedScores.Count; i += 2)
        {
            if (i + 1 >= savedScores.Count) break;
            var teamOne = savedScores[i];
            var teamTwo = savedScores[i + 1];
            var gs = new GameScoreModel(teamOne, teamTwo);
            gs.CalculateScore();
            Scores.Add(gs);
            TeamOneGameTotal += gs.TeamOneScore;
            TeamTwoGameTotal += gs.TeamTwoScore;
        }

        TeamOneGameTotal = Scores.Sum(s => s.TeamOneScore);
        TeamTwoGameTotal = Scores.Sum(s => s.TeamTwoScore);
    }

    partial void OnTeamOneScoreInputChanged(string value)
    {
        ClearStiljaComponents();

        if (!_canAddScore) return;

        if (!int.TryParse(value, out var parsed) || parsed > Maxscore)
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

        if (!int.TryParse(value, out var parsed) || parsed > Maxscore)
        {
            TeamTwoScoreInput = "0";
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
        if (value) TeamTwoBela = false;
    }

    partial void OnTeamTwoBelaChanged(bool value)
    {
        if (value) TeamOneBela = false;
    }

    [RelayCommand]
    private void AddScore()
    {
        if (IsInputValid())
        {
            var teamOneModel = new ScoreModel
            {
                ScoreOnly = _teamOneScore,
                Bela = TeamOneBela ? 20 : 0,
                Call = TeamOneCall,
                IsCallChecked = TeamOneCallCheck,
                IsStilja = TeamOneStilja
            };
            var teamTwoModel = new ScoreModel
            {
                ScoreOnly = _teamTwoScore,
                Bela = TeamTwoBela ? 20 : 0,
                Call = TeamTwoCall,
                IsCallChecked = TeamTwoCallCheck,
                IsStilja = TeamTwoStilja
            };

            var gameScore = new GameScoreModel(teamOneModel, teamTwoModel);
            gameScore.CalculateScore();
            Scores.Insert(0, gameScore);
            _db.AddGameScore(gameScore);

            TeamOneGameTotal += gameScore.TeamOneScore;
            TeamTwoGameTotal += gameScore.TeamTwoScore;
            CheckForWin();
            ClearInputs();
        }
    }

    private void CheckForWin()
    {
        if (TeamOneGameTotal >= GameSettings.TargetScore)
        {
            App.Current?.MainPage?.DisplayAlert("Pobjeda", $"Pojedio je tim \"{GameSettings.TeamOne?.Name}\"!!", "Nova Igra");
            NewGame();
        }
        else if (TeamTwoGameTotal >= GameSettings.TargetScore)
        {
            App.Current?.MainPage?.DisplayAlert("Pobjeda", $"Pojedio je tim \"{GameSettings.TeamTwo?.Name}\"!!", "Nova Igra");
            NewGame();
        }
    }

    private void ClearInputs()
    {
        _canAddScore = false;
        TeamOneCallCheck = false;
        TeamTwoCallCheck = false;
        TeamOneScoreInput = "0";
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
        return int.TryParse(TeamOneScoreInput, out _teamOneScore) &&
               int.TryParse(TeamTwoScoreInput, out _teamTwoScore) &&
               (_teamOneScore != 0 || _teamTwoScore != 0);
    }

    private void NewGame()
    {
        _db.ClearCurrentGameScores();
        Scores.Clear();
        TeamOneGameTotal = 0;
        TeamTwoGameTotal = 0;
        ClearInputs();
    }

    [RelayCommand]
    private async Task NavigateToSettings()
    {
        await Shell.Current.GoToAsync($"///SettingsView");
    }

    [RelayCommand]
    private async Task NavigateToHelp()
    {
        var popup = new HelpPopup();
        await App.Current.MainPage.ShowPopupAsync(popup);
    }

    [RelayCommand]
    private async Task StartNewGame()
    {
        NewGame();
    }
}
