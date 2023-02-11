using System;

public class PlayerPreparationState : GameBaseState
{
    public PlayerPreparationState(IGameStateSwitcher gameStateSwitcher)
        : base(gameStateSwitcher)
    {

    }

    public override void Enter()
    {
        
    }

    public override void Exit()
    {
        _gameStateSwitcher.SwitchState<EnemyPreparationState>();
    }
}
