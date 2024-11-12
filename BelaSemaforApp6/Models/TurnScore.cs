namespace BelaSemaforApp6.Models;

public class TurnScore
{
    public int TeamOne { get; set; }
    public bool OneBela { get; set; }
    public int OneCall { get; set; }
    public int TeamTwo { get; set; }
    public bool TwoBela { get; set; }
    public int TwoCall { get; set; }
    public int TeamOneTotal { get; set; }
    public int TeamTwoTotal { get; set; }

    public TurnScore()
    {
        TeamScoreAddition();
    }

    private void TeamScoreAddition()
    {
        if (OneBela)
        {
            TeamOneTotal = TeamOne + 20 + OneCall;
            TeamTwoTotal = TeamTwo + TwoCall;
        }
        else if (TwoBela)
        {
            TeamOneTotal = TeamOne + OneCall;
            TeamTwoTotal = TeamTwo + 20 + TwoCall;
        }
        else
        {
            TeamOneTotal = TeamOne + OneCall;
            TeamTwoTotal = TeamTwo + TwoCall;
        }
    }
}