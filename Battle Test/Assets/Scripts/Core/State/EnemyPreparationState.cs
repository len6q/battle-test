using System;

public class EnemyPreparationState : GameBaseState
{
    public EnemyPreparationState(IGameStateSwitcher gameStateSwitcher)
        : base(gameStateSwitcher)
    {

    }

    public override void Enter()
    {
        throw new NotImplementedException();
    }

    public override void Exit()
    {
        _gameStateSwitcher.SwitchState<FightState>();
    }
}
