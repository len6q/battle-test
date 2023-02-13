using System.Collections.Generic;
using System.Linq;
using Zenject;

public sealed class Game : IGameStateSwitcher, IInitializable, ITickable
{            
    private readonly DefenderHud _defenderHud;
    private readonly Player _player;
    private readonly Enemy _enemy;
    private readonly GameConfig _config;
    private readonly FightArea _fightArea;

    private GameBaseState _currentState;
    private List<GameBaseState> _allStates;

    public Game(
        Player player,
        Enemy enemy,
        DefenderHud defenderHud,
        GameConfig gameConfig,
        FightArea fightArea)
    {
        _player = player;
        _enemy = enemy;
        _defenderHud = defenderHud;
        _config = gameConfig;
        _fightArea = fightArea;
    }

    public void Initialize()
    {        
        _allStates = new List<GameBaseState>()
        {
            new PlayerPreparationState(_player, _enemy, _defenderHud, _config, this),
            new EnemyPreparationState(_player, _enemy, _defenderHud, _config, this),
            new FightState(_player, _enemy, _defenderHud, _config, this, _fightArea),
            new GameOverState(_player, _enemy, _defenderHud, _config, this)
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
