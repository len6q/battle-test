using UnityEngine;

public class FightState : GameBaseState
{
    public FightState(
        Player player, Enemy enemy,
        DefenderHud defenderHud, GameConfig config,
        IGameStateSwitcher gameStateSwitcher)
        : base(player, enemy, defenderHud, config, gameStateSwitcher)
    {
    }

    public override void Enter()
    {
        _player.Fight();
        _enemy.Fight();
        Debug.Log(this);

        if (_player.Health <= 0 || _enemy.Health <= 0)
            _gameStateSwitcher.SwitchState<GameOverState>();
        else
            _gameStateSwitcher.SwitchState<PlayerPreparationState>();
    }

    public override void Exit()
    {
        _defenderHud.Refresh();
    }

    public override void Tick()
    {        
    }
}
