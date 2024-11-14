namespace BelaSemaforApp6.Models;

public class TurnScoreModel
{
    public int TeamOneScoreOnly { get; set; }
    public bool IsTeamOneBelaChecked { get; set; }
    public int TeamOneCall { get; set; }
    public bool IsTeamOneCallChecked  { get; set; }
    public int TeamOneTurnTotal { get; set; }
    public int TeamTwoScoreOnly { get; set; }
    public bool IsTeamTwoBelaChecked { get; set; }
    public int TeamTwoCall { get; set; }
    public bool IsTeamTwoCallChecked  { get; set; }
    public int TeamTwoTurnTotal { get; set; }

    
}