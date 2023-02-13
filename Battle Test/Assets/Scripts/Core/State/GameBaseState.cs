public abstract class GameBaseState
{
    protected readonly Player _player;
    protected readonly Enemy _enemy;        
    protected readonly IGameStateSwitcher _gameStateSwitcher;    

    protected GameBaseState(IGameStateSwitcher gameStateswitcher, Player player, Enemy enemy)        
    {
        _player = player;
        _enemy = enemy;                
        _gameStateSwitcher = gameStateswitcher;
    }

    public abstract void Tick();

    public abstract void Enter();
    
    public abstract void Exit();
}
