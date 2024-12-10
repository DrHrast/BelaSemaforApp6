using System.Collections.ObjectModel;
using BelaSemaforApp6.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Configuration;
using Microsoft.Maui.Layouts;

namespace BelaSemaforApp6.ViewModels;

partial class SettingsViewModel : ObservableObject
{
    [ObservableProperty] private AppSettingsModel _appSettings;
    [ObservableProperty] private bool _isHeader = true;
    [ObservableProperty] private bool _isText;
    [ObservableProperty] private bool _isBackground;
    [ObservableProperty] private string? _playerName;
    [ObservableProperty] private string? _teamName;
    [ObservableProperty] private bool _canAddPlayer = false;
    [ObservableProperty] private bool _canAddTeam = false;
    [ObservableProperty] private ObservableCollection<Color> _primaryColors =
    [
        Color.Parse("#FFFFFF"), // Soft White
        Color.Parse("#A8A8A8"), // Light Gray
        Color.Parse("#303030"), // Charcoal Gray
    ];
    [ObservableProperty] private ObservableCollection<Color> _secondaryColors =
    [
        Color.Parse("#F5F5F5"), // Soft White
        Color.Parse("#D3D3D3"), // Light Gray
        Color.Parse("#A9A9A9"), // Medium Gray
        Color.Parse("#C3BEBE"), // Pastel Slate
        Color.Parse("#BEBEBE"), // Soft Gray
        Color.Parse("#E6D0D8"), // Pastel Thistle
        Color.Parse("#FFB6C1"), // Light Pink
        Color.Parse("#C96F6F"), // Soft Blood Red
        Color.Parse("#C86464"), // Pastel Dark Red
        Color.Parse("#F9A76C"), // Pastel Orange
        Color.Parse("#FBE89F"), // Pastel Gold
        Color.Parse("#FFF799"), // Pastel Yellow
        Color.Parse("#A8D5BA"), // Pastel Green
        Color.Parse("#B2DAD2"), // Pastel Sea Green
        Color.Parse("#9FD5DB"), // Pastel Turquoise
        Color.Parse("#A4C8EF"), // Pastel Blue
        Color.Parse("#B5A5E9"), // Pastel Purple
        Color.Parse("#A89FCF")  // Pastel Indigo
    ];

    public List<DisplayScoreModel> DisplayScore { get; set; } = new()
    {
        new DisplayScoreModel { Score = 301, IsSelected = false },
        new DisplayScoreModel { Score = 501, IsSelected = false },
        new DisplayScoreModel { Score = 701, IsSelected = false },
        new DisplayScoreModel { Score = 901, IsSelected = false },
        new DisplayScoreModel { Score = 1001, IsSelected = false },
        new DisplayScoreModel { Score = 1301, IsSelected = false }
    };
    
    public ObservableCollection<PlayerModel> Players { get; set; } = new();
    public ObservableCollection<PlayerModel> SelectedPlayers { get; set; } = new();
    public ObservableCollection<TeamModel> Teams { get; set; } = new();
    public ObservableCollection<TeamModel> SelectedTeams { get; set; } = new();


    public SettingsViewModel(IConfiguration config, AppSettingsModel appSettingSettings)
    {
        AppSettings = appSettingSettings;
    }
    
    partial void OnPlayerNameChanged(string? oldValue, string? newValue)
    {
        if (!string.IsNullOrWhiteSpace(newValue) && !Players.Any(x => x.Name == newValue))
        {
            CanAddPlayer = true;
        }
        else
        {
            CanAddPlayer = false;
        }
    }

    partial void OnTeamNameChanged(string? oldValue, string? newValue)
    {
        if (!string.IsNullOrWhiteSpace(newValue) && !Teams.Any(x => x.Name == newValue) && SelectedPlayers.Count == 2)
        {
            CanAddTeam = true;
        }
        else CanAddTeam = false;
    }

    private void ClearSelectedPlayers()
    {
        Players.Add(SelectedPlayers[0]);
        Players.Add(SelectedPlayers[1]);
        SelectedPlayers.Clear();
    }

    [RelayCommand]
    private async void NavigateToGame()
    {
        await Shell.Current.GoToAsync("///GameView");
    }
    
    [RelayCommand]
    private void SetPrimary(Color selectedColor)
    {
        AppSettings.PrimaryColor = selectedColor;
    }

    [RelayCommand]
    private void SetSecondary(Color selectedColor)
    {
        AppSettings.SecondaryColor = selectedColor;
    }

    [RelayCommand]
    private void SetTargetScore(int selectedScore)
    {
        AppSettings.TargetScore = selectedScore;
    }

    [RelayCommand]
    private void AddPlayer()
    {
        Players.Add( new PlayerModel {Name = PlayerName});
        PlayerName = "";
    }

    [RelayCommand]
    private void SelectPlayer(PlayerModel player)
    {
        if (SelectedPlayers.Count() < 2)
        {
            SelectedPlayers.Add(player);
            Players.Remove(player);
        }
        else
        {
            var temp = SelectedPlayers[0];
            SelectedPlayers.RemoveAt(0);
            Players.Add(temp);
            SelectedPlayers.Add(player);
            Players.Remove(player);
        }
    }

    [RelayCommand]
    private void DeselectPlayer(PlayerModel player)
    {
        Players.Add(player);
        SelectedPlayers.Remove(player);
    }

    [RelayCommand]
    private void AddTeam()
    {
        Teams.Add( new TeamModel
        {
            Name = TeamName,
            FirstPlayer = SelectedPlayers[0].Name,
            SecondPlayer = SelectedPlayers[1].Name
        });
        ClearSelectedPlayers();
    }

    [RelayCommand]
    private void SelectTeam(TeamModel team)
    {
        if (SelectedTeams.Count() < 2)
        {
            SelectedTeams.Add(team);
            Teams.Remove(team);
        }
        else
        {
            var temp = SelectedTeams[0];
            SelectedTeams.RemoveAt(0);
            Teams.Add(temp);
            SelectedTeams.Add(team);
            Teams.Remove(team);
        }
    }

    [RelayCommand]
    private void DeselectTeam(TeamModel team)
    {
        Teams.Add(team);
        SelectedTeams.Remove(team);
    }
}