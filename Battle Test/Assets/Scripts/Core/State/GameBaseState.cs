public abstract class GameBaseState
{
    protected readonly Player _player;
    protected readonly Enemy _enemy;
    protected readonly DefenderHud _defenderHud;
    protected readonly GameConfig _config;
    protected readonly IGameStateSwitcher _gameStateSwitcher;    

    protected GameBaseState(
        Player player,
        Enemy enemy, 
        DefenderHud defenderHud,
        GameConfig config,
        IGameStateSwitcher gameStateswitcher)
    {
        _player = player;
        _enemy = enemy;
        _defenderHud = defenderHud;
        _config = config;
        _gameStateSwitcher = gameStateswitcher;
    }

    public abstract void Tick();

    public abstract void Enter();
    
    public abstract void Exit();
}
