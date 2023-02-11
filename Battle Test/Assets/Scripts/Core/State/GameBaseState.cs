public abstract class GameBaseState
{
    protected readonly IGameStateSwitcher _gameStateSwitcher;
    
    protected GameBaseState(IGameStateSwitcher gameStateswitcher)
    {
        _gameStateSwitcher = gameStateswitcher;
    }

    public abstract void Enter();
    
    public abstract void Exit();
}
