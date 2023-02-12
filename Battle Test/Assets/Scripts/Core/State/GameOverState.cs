using UnityEngine;

public class GameOverState : GameBaseState
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
    }

    public override void Exit()
    {
        
    }

    public override void Tick()
    {

    }
}
