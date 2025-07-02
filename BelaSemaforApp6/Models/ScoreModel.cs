using SQLite;

namespace BelaSemaforApp6.Models;

public class ScoreModel
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int ScoreOnly { get; set; }
    public int Bela { get; set; }
    public int Call { get; set; }
    public bool IsCallChecked  { get; set; }
    public bool IsStilja { get; set; }
    
}