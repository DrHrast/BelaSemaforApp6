namespace BelaSemaforApp6.Models;

public class TeamsModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int PlayerOneId { get; set; }
    public int PlayerTwoId { get; set; }
}