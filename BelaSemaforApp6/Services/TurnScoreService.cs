using BelaSemaforApp6.Models;

namespace BelaSemaforApp6.Services;

public class TurnScoreService
{
    private TurnScoreModel _turnScoreModel;
    private const int BelaValue = 20;

    public TurnScoreService(TurnScoreModel turnScoreModel)
    {
        _turnScoreModel = turnScoreModel;
    }
    
    public TurnScoreModel GetModel()
    {
        CalculateScores();
        return _turnScoreModel;
    }

    private void CalculateScores()
    {
        CalculateTurnTotal();
        CheckCallConditions();
    }

    private void CalculateTurnTotal()
    {
        _turnScoreModel.TeamOneTurnTotal = _turnScoreModel.IsTeamOneBelaChecked
            ? _turnScoreModel.TeamOneScoreOnly + BelaValue + _turnScoreModel.TeamOneCall
            : _turnScoreModel.TeamOneScoreOnly + _turnScoreModel.TeamOneCall;
        _turnScoreModel.TeamTwoTurnTotal = _turnScoreModel.IsTeamTwoBelaChecked
            ? _turnScoreModel.TeamTwoScoreOnly + BelaValue + _turnScoreModel.TeamTwoCall
            : _turnScoreModel.TeamTwoScoreOnly + _turnScoreModel.TeamTwoCall;
    }
    
    private void CheckCallConditions()
    {
        if (_turnScoreModel.IsTeamOneCallChecked)
        {
            if (!CheckTeamOnePassCondition())
            {
                _turnScoreModel.TeamTwoTurnTotal += _turnScoreModel.TeamOneTurnTotal;
                _turnScoreModel.TeamOneTurnTotal = 0;
            }
        }
        else if (_turnScoreModel.IsTeamTwoCallChecked)
        {
            if (!CheckTeamTwoPassCondition())
            {
                _turnScoreModel.TeamOneTurnTotal += _turnScoreModel.TeamTwoTurnTotal;
                _turnScoreModel.TeamTwoTurnTotal = 0;
            }
        }
    }

    private bool CheckTeamOnePassCondition()
    {
        return _turnScoreModel.TeamOneTurnTotal > _turnScoreModel.TeamTwoTurnTotal;
    }

    private bool CheckTeamTwoPassCondition()
    {
        return _turnScoreModel.TeamTwoTurnTotal > _turnScoreModel.TeamOneTurnTotal;
    }
}