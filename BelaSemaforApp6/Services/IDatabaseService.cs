using BelaSemaforApp6.Models;
using SQLite;

public interface IDatabaseService
{
    Task InitAsync();

    // Players
    Task<List<PlayerModel>> GetPlayersAsync();
    Task<int> AddPlayerAsync(PlayerModel player);
    Task<int> DeletePlayerAsync(PlayerModel player);

    // Teams
    Task<List<TeamModel>> GetTeamsAsync();
    Task<int> AddTeamAsync(TeamModel team);
    Task<int> DeleteTeamAsync(TeamModel team);

    // Scores
    Task<List<GameScoreModel>> GetScoresAsync();
    Task<int> AddScoreAsync(GameScoreModel score);
    Task<int> ClearScoresAsync();

    // Settings (optional if persisted)
    Task<GameSettingsModel?> GetGameSettingsAsync();
    Task<int> SaveGameSettingsAsync(GameSettingsModel settings);
}
public class DatabaseService : IDatabaseService
{
    private SQLiteAsyncConnection _db;

    public async Task InitAsync()
    {
        if (_db != null) return;

        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "bela_semafor.db3");
        _db = new SQLiteAsyncConnection(dbPath);
        await _db.CreateTableAsync<PlayerModel>();
        await _db.CreateTableAsync<TeamModel>();
        await _db.CreateTableAsync<GameSettingsModel>();
        // etc.
    }

    public Task<List<PlayerModel>> GetPlayersAsync() =>
        _db.Table<PlayerModel>().ToListAsync();

    public Task<int> AddPlayerAsync(PlayerModel player) =>
        _db.InsertAsync(player);

    public Task<int> DeletePlayerAsync(PlayerModel player) =>
        _db.DeleteAsync(player);

    public Task<List<TeamModel>> GetTeamsAsync() =>
        _db.Table<TeamModel>().ToListAsync();

    public Task<int> AddTeamAsync(TeamModel team) =>
        _db.InsertAsync(team);

    public Task<int> DeleteTeamAsync(TeamModel team) =>
        _db.DeleteAsync(team);

    public Task<int> AddScoreAsync(GameScoreModel score) =>
        _db.InsertAsync(score);

    public Task<int> ClearScoresAsync() =>
        _db.DeleteAllAsync<GameScoreModel>();

    public async Task<GameSettingsModel?> GetGameSettingsAsync() =>
        await _db.Table<GameSettingsModel>().FirstOrDefaultAsync();

    public Task<int> SaveGameSettingsAsync(GameSettingsModel settings) =>
        _db.InsertOrReplaceAsync(settings);

    public Task<List<GameScoreModel>> GetScoresAsync()
    {
        throw new NotImplementedException();
    }
}