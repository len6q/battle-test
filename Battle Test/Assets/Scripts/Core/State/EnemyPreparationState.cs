public class EnemyPreparationState : GameBaseState
{
    private const int PREPARATION_TIME = 15;

    public EnemyPreparationState(Player player, Enemy enemy, DefenderHud defenderHud, IGameStateSwitcher gameStateSwitcher)
        : base(player, enemy, defenderHud, gameStateSwitcher)
    {
    }

    public override void Enter()
    {
        
    }

    public override void Exit()
    {
        _gameStateSwitcher.SwitchState<FightState>();
    }

    public override void Tick()
    {
        
    }
}
