using System.Collections.Generic;
using System.Linq;
using Zenject;

public sealed class Game : IGameStateSwitcher, IInitializable, ITickable
{            
    private readonly PreparationHud _preparationHud;
    private readonly FightHud _fightHud;
    private readonly GameOverHud _gameOverHud;
    private readonly Player _player;
    private readonly Enemy _enemy;
    private readonly GameConfig _config;
    private readonly FightArea _fightArea;

    private GameBaseState _currentState;
    private List<GameBaseState> _allStates;

    public Game(
        Player player, Enemy enemy,
        PreparationHud preparationHud, FightHud fightHud, GameOverHud gameOverHud,
        GameConfig gameConfig, FightArea fightArea)
    {
        _player = player;
        _enemy = enemy;
        _preparationHud = preparationHud;
        _fightHud = fightHud;
        _gameOverHud = gameOverHud;
        _config = gameConfig;
        _fightArea = fightArea;
    }

    public void Initialize()
    {        
        _allStates = new List<GameBaseState>()
        {
            new StartupState(this, _player, _enemy, _preparationHud, _fightHud, _gameOverHud),
            new PlayerPreparationState(this, _player, _enemy, _config, _preparationHud),
            new EnemyPreparationState(this, _player, _enemy, _config, _preparationHud),
            new FightState(this, _player, _enemy, _fightArea, _fightHud),
            new GameOverState(this, _player, _enemy, _gameOverHud)
        };
        _currentState = _allStates[0];
        _currentState.Enter();
    }

    public void Tick()
    {        
        _currentState.Tick();
    }

    public void SwitchState<T>() where T : GameBaseState
    {
        _currentState.Exit();
        var state = _allStates.FirstOrDefault(s => s is T);
        _currentState = state;
        _currentState.Enter();
    }
}
