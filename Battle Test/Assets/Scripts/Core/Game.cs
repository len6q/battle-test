using System.Collections.Generic;
using System.Linq;
using Zenject;

public class Game : IGameStateSwitcher, IInitializable, ITickable
{            
    private readonly DefenderHud _defenderHud;
    private readonly Player _player;
    private readonly Enemy _enemy;

    private GameBaseState _currentState;
    private List<GameBaseState> _allStates;

    public Game(Player player, Enemy enemy, DefenderHud defenderHud)
    {
        _player = player;
        _enemy = enemy;
        _defenderHud = defenderHud;        
    }

    public void Initialize()
    {        
        _allStates = new List<GameBaseState>()
        {
            new PlayerPreparationState(_player, _enemy, _defenderHud, this),
            new EnemyPreparationState(_player, _enemy, _defenderHud, this),
            new FightState(_player, _enemy, _defenderHud, this),
            new GameOverState(_player, _enemy, _defenderHud, this)
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
