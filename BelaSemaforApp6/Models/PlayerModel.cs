using SQLite;

namespace BelaSemaforApp6.Models;

public class PlayerModel
{
    [PrimaryKey] [AutoIncrement]
    public string? Name { get; set; }
}