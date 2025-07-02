using SQLite;
using BelaSemaforApp6.Models;

namespace BelaSemaforApp6;

public class DatabaseManager
{
    private readonly string _dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "belaDb.db3");
    private readonly SQLiteConnection _database;

    public DatabaseManager()
    {
        _database = new SQLiteConnection(_dbPath);
        _database.CreateTable<AppSettingsModel>();
        _database.CreateTable<GameSettingsModel>();
        _database.CreateTable<PlayerModel>();
        _database.CreateTable<TeamModel>();
        _database.CreateTable<ScoreModel>();
    }

    // AppSettings
    public AppSettingsModel? GetAppSettings() => _database.Table<AppSettingsModel>().FirstOrDefault();
    public void SaveAppSettings(AppSettingsModel settings)
    {
        _database.DeleteAll<AppSettingsModel>();
        _database.Insert(settings);
    }

    // GameSettings
    public GameSettingsModel? GetGameSettings() => _database.Table<GameSettingsModel>().FirstOrDefault();
    public void SaveGameSettings(GameSettingsModel settings)
    {
        _database.DeleteAll<GameSettingsModel>();
        _database.Insert(settings);
    }

    // Players
    public List<PlayerModel> GetPlayers() => _database.Table<PlayerModel>().ToList();
    public void AddPlayer(PlayerModel player) => _database.Insert(player);
    public void DeletePlayer(PlayerModel player) => _database.Delete(player);

    // Teams
    public List<TeamModel> GetTeams() => _database.Table<TeamModel>().ToList();
    public void AddTeam(TeamModel team) => _database.Insert(team);
    public void DeleteTeam(TeamModel team) => _database.Delete(team);
    public TeamModel? GetTeamById(int id) => _database.Table<TeamModel>().FirstOrDefault(t => t.Id == id);

    // Game Scores
    public List<ScoreModel> GetGameScores() => _database.Table<ScoreModel>().ToList();
    public void AddGameScore(GameScoreModel gameScore)
    {
        _database.Insert(new ScoreModel
        {
            ScoreOnly = gameScore.TeamOneScore,
            Bela = gameScore._teamOne?.Bela ?? 0,
            Call = gameScore._teamOne?.Call ?? 0,
            IsCallChecked = gameScore._teamOne?.IsCallChecked ?? false,
            IsStilja = gameScore._teamOne?.IsStilja ?? false
        });

        _database.Insert(new ScoreModel
        {
            ScoreOnly = gameScore.TeamTwoScore,
            Bela = gameScore._teamTwo?.Bela ?? 0,
            Call = gameScore._teamTwo?.Call ?? 0,
            IsCallChecked = gameScore._teamTwo?.IsCallChecked ?? false,
            IsStilja = gameScore._teamTwo?.IsStilja ?? false
        });
    }

    public void ClearCurrentGameScores() => _database.DeleteAll<ScoreModel>();
}
