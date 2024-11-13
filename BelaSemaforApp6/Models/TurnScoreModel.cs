namespace BelaSemaforApp6.Models;

public class TurnScoreModel
{
    public int TeamOne { get; set; }
    public bool OneBela { get; set; }
    public int OneCall { get; set; }
    public int TeamTwo { get; set; }
    public bool TwoBela { get; set; }
    public int TwoCall { get; set; }
    public int TeamOneTotal => OneBela ? TeamOne + 20 + OneCall : TeamOne + OneCall;
    public int TeamTwoTotal => TwoBela ? TeamTwo + 20 + TwoCall : TeamTwo + TwoCall;

    
}