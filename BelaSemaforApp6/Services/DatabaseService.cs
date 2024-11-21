using BelaSemaforApp6.Models;
using SQLite;

namespace BelaSemaforApp6.Services;

public class DatabaseService
{
    private string _dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "bela_semafor.db3");
    private readonly SQLiteConnection _database;
    private readonly GameSettingsModel _gameSettingsModel;
    private readonly ColorsModel _colorsModel;

    public DatabaseService()
    {
        _database = new SQLiteConnection(_dbPath);
        _database.CreateTable<GameSettingsModel>();
        
        var settingsCount = _database.Table<GameSettingsModel>().Count();
        if (settingsCount == 0)
        {
            _database.Insert(new GameSettingsModel
            {
                Key = "_primaryColor",
                Value = $"{_colorsModel.PrimaryColor}"
            });

            _database.Insert(new GameSettingsModel
            {
                Key = "_secondaryColor",
                Value = $"{_colorsModel.SecondaryColor}"
            });

            _database.Insert(new GameSettingsModel
            {
                Key = "_gameScore",
                Value = "1001"
            });
        }
    }
}