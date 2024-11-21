using SQLite;

namespace BelaSemaforApp6.Models;

public class PlayersModel
{
    [PrimaryKey] [AutoIncrement]
    public int Id { get; set; }
    public string? Name { get; set; }
}