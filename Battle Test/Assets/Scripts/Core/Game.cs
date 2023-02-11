using System;
using System.Collections.Generic;
using System.Linq;
using Zenject;

public class Game : IGameStateSwitcher, IInitializable, IDisposable
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
            new PlayerPreparationState(this),
            new EnemyPreparationState(this),
            new FightState(this)
        };
        _currentState = _allStates[0];

        for (int i = 0; i < _defenderHud.CountAttackFields; i++)
        {
            AttackField attackField = _defenderHud.GetDesiredAttackField(i);
            BodyPart bodyPart = _player.GetDesiredPart(attackField.Type);
            attackField.OnSelected += bodyPart.Select;
            attackField.OnDeselected += bodyPart.Deselect;
            attackField.OnSetAttackValue += bodyPart.SetDamage;
        }

        for (int i = 0; i < _defenderHud.CountProtectionFields; i++)
        {
            ProtectionField protectionField = _defenderHud.GetDesiredProtectionField(i);
            BodyPart bodyPart = _enemy.GetDesiredPart(protectionField.Type);
            protectionField.OnSetProtectionValue += bodyPart.SetProtection;
        }
    }

    public void Dispose()
    {
        for (int i = 0; i < _defenderHud.CountAttackFields; i++)
        {
            AttackField attackField = _defenderHud.GetDesiredAttackField(i);
            BodyPart bodyPart = _player.GetDesiredPart(attackField.Type);
            attackField.OnSelected -= bodyPart.Select;
            attackField.OnDeselected -= bodyPart.Deselect;
            attackField.OnSetAttackValue -= bodyPart.SetDamage;
        }

        for (int i = 0; i < _defenderHud.CountProtectionFields; i++)
        {
            ProtectionField protectionField = _defenderHud.GetDesiredProtectionField(i);
            BodyPart bodyPart = _enemy.GetDesiredPart(protectionField.Type);
            protectionField.OnSetProtectionValue -= bodyPart.SetProtection;
        }
    }

    public void SwitchState<T>() where T : GameBaseState
    {
        _currentState.Exit();
        var state = _allStates.FirstOrDefault(s => s is T);
        _currentState = state;
        _currentState.Enter();
    }
}
