﻿using UnityEngine;

public class EnemyPreparationState : GameBaseState
{
    private float _preparationTime = 10f;

    private float PreparationTime
    {
        get => _preparationTime;
        set
        {
            _preparationTime = value;
            if (_preparationTime < 0)
            {
                SwitchState();
            }

            _defenderHud.SetValueOnTimer(Mathf.CeilToInt(_preparationTime));
        }
    }

    public EnemyPreparationState(Player player, Enemy enemy, DefenderHud defenderHud, IGameStateSwitcher gameStateSwitcher)
        : base(player, enemy, defenderHud, gameStateSwitcher)
    {
    }

    public override void Enter()
    {
        _defenderHud.OnClickPlayButton += SwitchState;

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
            protectionField.OnDeselected += bodyPart.Deselect;
        }                
    }

    public override void Exit()
    {
        _defenderHud.OnClickPlayButton -= SwitchState;

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
            protectionField.OnDeselected -= bodyPart.Deselect;
        }
    }

    public override void Tick()
    {
        PreparationTime -= Time.deltaTime;
    }

    private void SwitchState()
    {
        _gameStateSwitcher.SwitchState<FightState>();
    }
}
