using SQLite;

namespace BelaSemaforApp6.Models;

public class GameSettingsModel
{
    // Colors, GameScore
    [PrimaryKey] [AutoIncrement]
    public int Id { get; set; }
    public string? Key { get; set; } = "";
    public string? Value { get; set; } = "";
}