namespace BelaSemaforApp6.Models;

public class GameScoreModel
{
    public readonly ScoreModel? _teamOne;
    public readonly ScoreModel? _teamTwo;
    public int TargetScore = 301;
    private int GameScore => _teamOne.Call + _teamTwo.Call + _teamOne.Bela + _teamTwo.Bela + 162;
    public int TeamOneScore { get; private set; }
    public int TeamTwoScore { get; private set; }

    public GameScoreModel(ScoreModel? teamOne, ScoreModel? teamTwo)
    {
        _teamOne = teamOne;
        _teamTwo = teamTwo;

        TeamOneScore = _teamOne.ScoreOnly + _teamOne.Bela + _teamOne.Call;
        TeamTwoScore = _teamTwo.ScoreOnly + _teamTwo.Bela + _teamTwo.Call;
    }

    public void CalculateScore()
    {
        CheckFall();
    }

    private void CheckFall()
    {
        if (_teamOne.IsCallChecked && TeamOneScore > 0 && TeamOneScore < GameScore / 2)
        {
            TeamOneScore = 0;
            TeamTwoScore = GameScore;
        }

        if (_teamTwo.IsCallChecked && TeamTwoScore > 0 && TeamTwoScore < GameScore / 2)
        {
            TeamOneScore = GameScore;
            TeamTwoScore = 0;
        }
    }
}