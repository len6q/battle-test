public sealed class GameOverState : GameBaseState
{
    private readonly GameOverHud _gameOverHud;

    public GameOverState(
        IGameStateSwitcher gameStateSwitcher,
        Player player, Enemy enemy,
        GameOverHud gameOverHud)
        : base(gameStateSwitcher, player, enemy)
    {
        _gameOverHud = gameOverHud;
    }

    public override void Enter()
    {
        _player.Visible = false;
        _enemy.Visible = false;        

        _gameOverHud.Open();
        _gameOverHud.AddAction(SwitchState);

        if(_player.IsDead && _enemy.IsDead)
        {
            _gameOverHud.ShowDraw();
        }
        else if(_player.IsDead)
        {
            _gameOverHud.ShowEnemyWinner();
        }
        else
        {
            _gameOverHud.ShowPlayerWinner();
        }
    }

    public override void Exit()
    {
        _player.Visible = true;
        _enemy.Visible = true;
        _gameOverHud.RemoveAction(SwitchState);
        _gameOverHud.Close();
    }

    public override void Tick()
    {
    }
    
    private void SwitchState()
    {
        _gameStateSwitcher.SwitchState<StartupState>();
    }
}
