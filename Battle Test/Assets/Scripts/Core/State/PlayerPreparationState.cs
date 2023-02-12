using UnityEngine;

public class PlayerPreparationState : GameBaseState
{
    private const float CACHE_TIME = 5f;
    
    private float _preparationTime;

    private float PreparationTime
    {
        get => _preparationTime;
        set
        {
            _preparationTime = value;
            if(_preparationTime < 0)
            {
                SwitchState();                
            }
                        
            _defenderHud.SetValueOnTimer(Mathf.CeilToInt(_preparationTime));
        }
    }

    public PlayerPreparationState(Player player, Enemy enemy, DefenderHud defenderHud, IGameStateSwitcher gameStateSwitcher)
        : base(player, enemy, defenderHud, gameStateSwitcher)
    {        
    }

    public override void Enter()
    {
        Debug.Log(this);
        _preparationTime = CACHE_TIME;

        _defenderHud.OnClickPlayButton += SwitchState;        

        for (int i = 0; i < _defenderHud.CountAttackFields; i++)
        {
            AttackField attackField = _defenderHud.GetDesiredAttackField(i);
            BodyPart bodyPart = _enemy.GetDesiredPart(attackField.Type);
            attackField.OnSelected += bodyPart.Select;
            attackField.OnDeselected += bodyPart.Deselect;
            attackField.OnSetAttackValue += bodyPart.SetDamage;
        }

        for (int i = 0; i < _defenderHud.CountProtectionFields; i++)
        {
            ProtectionField protectionField = _defenderHud.GetDesiredProtectionField(i);
            BodyPart bodyPart = _player.GetDesiredPart(protectionField.Type);
            protectionField.OnSetProtectionValue += bodyPart.SetProtection;
            protectionField.OnDeselected += bodyPart.Deselect;
        }        
    }

    public override void Exit()
    {        
        _defenderHud.Refresh();
        _defenderHud.OnClickPlayButton -= SwitchState;

        for (int i = 0; i < _defenderHud.CountAttackFields; i++)
        {
            AttackField attackField = _defenderHud.GetDesiredAttackField(i);
            BodyPart bodyPart = _enemy.GetDesiredPart(attackField.Type);
            attackField.OnSelected -= bodyPart.Select;
            attackField.OnDeselected -= bodyPart.Deselect;
            attackField.OnSetAttackValue -= bodyPart.SetDamage;
        }

        for (int i = 0; i < _defenderHud.CountProtectionFields; i++)
        {
            ProtectionField protectionField = _defenderHud.GetDesiredProtectionField(i);
            BodyPart bodyPart = _player.GetDesiredPart(protectionField.Type);
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
        _gameStateSwitcher.SwitchState<EnemyPreparationState>();
    }
}
