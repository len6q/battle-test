using UnityEngine;

public sealed class GameOverState : GameBaseState
{
    public GameOverState(
        Player player, Enemy enemy,
        DefenderHud defenderHud, GameConfig config,
        IGameStateSwitcher gameStateSwitcher)
        : base(player, enemy, defenderHud, config, gameStateSwitcher)
    {
    }

    public override void Enter()
    {
        Debug.Log(this);
        if(_player.IsDead && _enemy.IsDead)
        {
            Debug.Log("Draw");
        }
        else if(_player.IsDead)
        {
            Debug.Log($"{_enemy.name} is win!");
        }
        else
        {
            Debug.Log($"{_player.name} is win!");
        }
    }

    public override void Exit()
    {
        
    }

    public override void Tick()
    {

    }
}
