using System.Collections;
using UnityEngine;

public class FightState : GameBaseState
{
    private readonly FightArea _fightArea;

    public FightState(
        Player player, Enemy enemy,
        DefenderHud defenderHud, GameConfig config,
        IGameStateSwitcher gameStateSwitcher, FightArea fightArea)
        : base(player, enemy, defenderHud, config, gameStateSwitcher)
    {
        _fightArea = fightArea;
    }

    public override void Enter()
    {
        _fightArea.OnFightResult += SwitchState;
        _fightArea.StartCoroutine(_fightArea.CheckGameState());
        
        Debug.Log(this);
    }

    public override void Exit()
    {
        _defenderHud.Refresh();
        _fightArea.OnFightResult += SwitchState;
    }

    public override void Tick()
    {        
    }

    private void SwitchState(bool isGameOver)
    {
        if (isGameOver)        
            _gameStateSwitcher.SwitchState<GameOverState>();        
        else        
            _gameStateSwitcher.SwitchState<PlayerPreparationState>();        
    }
}
