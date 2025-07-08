using System.Collections.ObjectModel;
using BelaSemaforApp6.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Configuration;


namespace BelaSemaforApp6.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    [ObservableProperty] private AppSettingsModel _appSettings;
    [ObservableProperty] private GameSettingsModel _gameSettings;

    [ObservableProperty] private string? _playerName;
    [ObservableProperty] private string? _teamName;
    [ObservableProperty] private bool _canAddPlayer;
    [ObservableProperty] private bool _canAddTeam;

    public ObservableCollection<PlayerModel> Players { get; } = new();
    public ObservableCollection<PlayerModel> SelectedPlayers { get; } = new();
    public ObservableCollection<TeamModel> Teams { get; } = new();
    public ObservableCollection<TeamModel> SelectedTeams { get; } = new();

    public ObservableCollection<Color> PrimaryColors { get; } =
    [
        Color.Parse("#FFFFFF"), Color.Parse("#A8A8A8"), Color.Parse("#303030")
    ];

    public ObservableCollection<Color> SecondaryColors { get; } =
    [
        Color.Parse("#F5F5F5"), Color.Parse("#D3D3D3"), Color.Parse("#A9A9A9"),
        Color.Parse("#C3BEBE"), Color.Parse("#BEBEBE"), Color.Parse("#E6D0D8"),
        Color.Parse("#FFB6C1"), Color.Parse("#C96F6F"), Color.Parse("#C86464"),
        Color.Parse("#F9A76C"), Color.Parse("#FBE89F"), Color.Parse("#FFF799"),
        Color.Parse("#A8D5BA"), Color.Parse("#B2DAD2"), Color.Parse("#9FD5DB"),
        Color.Parse("#A4C8EF"), Color.Parse("#B5A5E9"), Color.Parse("#A89FCF")
    ];

    public List<DisplayScoreModel> DisplayScore { get; } = new()
    {
        new() { Score = 301 }, new() { Score = 501 }, new() { Score = 701 },
        new() { Score = 901 }, new() { Score = 1001 }, new() { Score = 1301 }
    };

    private readonly DatabaseManager _db;

    public SettingsViewModel(AppSettingsModel appSet, GameSettingsModel gameSet, DatabaseManager db)
    {
        _db = db;
        AppSettings = appSet;
        GameSettings = gameSet;

        Task.Run(LoadSettingsAsync);
    }

    partial void OnPlayerNameChanged(string? oldValue, string? newValue)
    {
        CanAddPlayer = !string.IsNullOrWhiteSpace(newValue) && !Players.Any(p => p.Name == newValue);
    }

    partial void OnTeamNameChanged(string? oldValue, string? newValue)
    {
        CanAddTeam = !string.IsNullOrWhiteSpace(newValue)
                     && !Teams.Any(t => t.Name == newValue)
                     && SelectedPlayers.Count == 2;
    }

    private void LoadSettingsAsync()
    {
        var savedAppSettings = _db.GetAppSettings();
        if (savedAppSettings != null)
        {
            AppSettings.PrimaryColor = savedAppSettings.PrimaryColor;
            AppSettings.SecondaryColor = savedAppSettings.SecondaryColor;
        }

        var savedGameSettings = _db.GetGameSettings();
        if (savedGameSettings != null)
        {
            GameSettings.TargetScore = savedGameSettings.TargetScore;
            GameSettings.TeamOneId = savedGameSettings.TeamOneId;
            GameSettings.TeamTwoId = savedGameSettings.TeamTwoId;
        }

        MainThread.BeginInvokeOnMainThread(() =>
        {
            var allPlayers = _db.GetPlayers();
            foreach (var player in allPlayers)
                Players.Add(player);

            var allTeams = _db.GetTeams();
            foreach (var team in allTeams)
            {
                team.FirstPlayer = allPlayers.FirstOrDefault(p => p.Id == team.PlayerOneId)?.Name;
                team.SecondPlayer = allPlayers.FirstOrDefault(p => p.Id == team.PlayerTwoId)?.Name;
                Teams.Add(team);
            }

            // Set selected teams
            if (GameSettings.TeamOneId > 0)
            {
                var teamOne = Teams.FirstOrDefault(t => t.Id == GameSettings.TeamOneId);
                if (teamOne != null)
                {
                    GameSettings.TeamOne = teamOne;
                    SelectedTeams.Add(teamOne);
                    Teams.Remove(teamOne);
                }
            }

            if (GameSettings.TeamTwoId > 0)
            {
                var teamTwo = Teams.FirstOrDefault(t => t.Id == GameSettings.TeamTwoId);
                if (teamTwo != null)
                {
                    GameSettings.TeamTwo = teamTwo;
                    SelectedTeams.Add(teamTwo);
                    Teams.Remove(teamTwo);
                }
            }

            foreach (var score in DisplayScore)
                score.IsSelected = score.Score == GameSettings.TargetScore ? false : true;
        });
    }


    [RelayCommand]
    private void SetPrimary(Color selectedColor)
    {
        AppSettings.PrimaryColor = selectedColor;
        _db.SaveAppSettings(AppSettings);
    }

    [RelayCommand]
    private void SetSecondary(Color selectedColor)
    {
        AppSettings.SecondaryColor = selectedColor;
        _db.SaveAppSettings(AppSettings);
    }

    [RelayCommand]
    private void SetTargetScore(int selectedScore)
    {
        foreach (var score in DisplayScore)
            score.IsSelected = true;

        var selected = DisplayScore.FirstOrDefault(s => s.Score == selectedScore);
        if (selected != null)
            selected.IsSelected = false;

        GameSettings.TargetScore = selectedScore;
        _db.SaveGameSettings(GameSettings);
    }


    [RelayCommand]
    private void AddPlayer()
    {
        var player = new PlayerModel { Name = PlayerName };
        _db.AddPlayer(player);
        Players.Add(player);
        PlayerName = string.Empty;
    }

    [RelayCommand]
    private void SelectPlayer(PlayerModel player)
    {
        if (SelectedPlayers.Count < 2)
        {
            SelectedPlayers.Add(player);
            Players.Remove(player);
        }
        else
        {
            var toReturn = SelectedPlayers[0];
            SelectedPlayers.RemoveAt(0);
            Players.Add(toReturn);
            SelectedPlayers.Add(player);
            Players.Remove(player);
        }
    }

    [RelayCommand]
    private void DeselectPlayer(PlayerModel player)
    {
        SelectedPlayers.Remove(player);
        Players.Add(player);
    }

    [RelayCommand]
    private void AddTeam()
    {
        var team = new TeamModel
        {
            Name = TeamName,
            PlayerOneId = SelectedPlayers[0].Id,
            PlayerTwoId = SelectedPlayers[1].Id,
            FirstPlayer = SelectedPlayers[0].Name,
            SecondPlayer = SelectedPlayers[1].Name
        };

        _db.AddTeam(team);
        Teams.Add(team);
        TeamName = string.Empty;

        foreach (var player in SelectedPlayers.ToList())
        {
            Players.Add(player);
        }

        SelectedPlayers.Clear();
    }

    [RelayCommand]
    private void SelectTeam(TeamModel team)
    {
        if (SelectedTeams.Count < 2)
        {
            SelectedTeams.Add(team);
            Teams.Remove(team);
        }
        else
        {
            var toReturn = SelectedTeams[0];
            SelectedTeams.RemoveAt(0);
            Teams.Add(toReturn);
            SelectedTeams.Add(team);
            Teams.Remove(team);
        }

        if (SelectedTeams.Count > 1)
        {
            GameSettings.TeamOne = SelectedTeams[0];
            GameSettings.TeamTwo = SelectedTeams[1];
        }
        else
        {
            GameSettings.TeamTwo = SelectedTeams[0];
        }
        _db.SaveGameSettings(GameSettings);
    }

    [RelayCommand]
    private void DeselectTeam(TeamModel team)
    {
        SelectedTeams.Remove(team);
        Teams.Add(team);
    }

    [RelayCommand]
    private async Task NavigateToGame()
    {
        await Shell.Current.GoToAsync("///GameView");
    }
}
