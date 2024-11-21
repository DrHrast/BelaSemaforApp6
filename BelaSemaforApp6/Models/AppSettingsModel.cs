using SQLite;

namespace BelaSemaforApp6.Models;

public class AppSettingsModel
{
    public string PrimaryColor { get; set; }
    public string SecondaryColor { get; set; }
    public int GameScore { get; set; }
}