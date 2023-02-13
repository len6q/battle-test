public sealed class FightState : GameBaseState
{
    private readonly FightHud _fightHud;
    private readonly FightArea _fightArea;

    public FightState(
        IGameStateSwitcher gameStateSwitcher,
        Player player, Enemy enemy,
        FightArea fightArea, FightHud fightHud)
        : base(gameStateSwitcher, player, enemy)
    {
        _fightHud = fightHud;
        _fightArea = fightArea;
    }

    public override void Enter()
    {        
        _fightHud.Open();

        _fightHud.ShowPlayerHealth(_player.Health);
        _fightHud.ShowEnemyHealth(_enemy.Health);

        RegistrationEvents();
        _fightArea.StartCoroutine(_fightArea.Fight());        
    }

    public override void Exit()
    {
        _fightHud.Close();

        DeregistrationEvents();
        _player.Refresh();
        _enemy.Refresh();        
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

    private void RegistrationEvents()
    {
        _player.OnTakenDamage += _fightHud.ShowPlayerHealth;
        _enemy.OnTakenDamage += _fightHud.ShowEnemyHealth;

        _fightArea.OnFight += SwitchState;
    }

    private void DeregistrationEvents()
    {
        _player.OnTakenDamage -= _fightHud.ShowPlayerHealth;
        _enemy.OnTakenDamage -= _fightHud.ShowEnemyHealth;

        _fightArea.OnFight -= SwitchState;
    }
}
