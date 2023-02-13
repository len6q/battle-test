public sealed class StartupState : GameBaseState
{
    private readonly PreparationHud _preparationHud;
    private readonly FightHud _fightHud;
    private readonly GameOverHud _gameOverHud;

    public StartupState(
        IGameStateSwitcher gameStateSwitcher,
        Player player, Enemy enemy,
        PreparationHud preparationHud, FightHud fightHud, GameOverHud gameOverHud)
        : base(gameStateSwitcher, player, enemy)
    {
        _preparationHud = preparationHud;
        _fightHud = fightHud;
        _gameOverHud = gameOverHud;
    }

    public override void Enter()
    {
        CloseWindows();
        InitFighters();

        _gameStateSwitcher.SwitchState<PlayerPreparationState>();
    }

    public override void Exit()
    {        
    }

    public override void Tick()
    {        
    }

    private void CloseWindows()
    {
        _preparationHud.Close();
        _fightHud.Close();
        _gameOverHud.Close();
    }

    private void InitFighters()
    {
        _player.SetHealth();
        _enemy.SetHealth();
    }
}
