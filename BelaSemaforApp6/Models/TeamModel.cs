using SQLite;

namespace BelaSemaforApp6.Models;

public class TeamModel
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string? Name { get; set; } = "Team";
    [Ignore]
    public string? FirstPlayer { get; set; }
    [Ignore]
    public string? SecondPlayer { get; set; }
    public int PlayerOneId { get; set; }
    public int PlayerTwoId { get; set; }
    [Ignore]
    public string? DisplayPlayers => $"{FirstPlayer}, {SecondPlayer}";
}