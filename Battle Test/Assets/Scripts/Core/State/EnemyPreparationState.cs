using UnityEngine;

public class EnemyPreparationState : GameBaseState
{
    private readonly float _cacheTime;

    private float _preparationTime;

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

    public EnemyPreparationState(
        Player player, Enemy enemy,
        DefenderHud defenderHud, GameConfig config,
        IGameStateSwitcher gameStateSwitcher)
        : base(player, enemy, defenderHud, config, gameStateSwitcher)
    {
        _cacheTime = _config.PreparationTime;
    }

    public override void Enter()
    {
        Debug.Log(this);
        _preparationTime = _cacheTime;
        
        InitFighters();
        RegistrationEvents();          
    }

    public override void Exit()
    {
        DeregistrationEvents();
    }

    public override void Tick()
    {
        PreparationTime -= Time.deltaTime;
    }

    private void SwitchState()
    {
        _gameStateSwitcher.SwitchState<FightState>();
    }

    private void RegistrationEvents()
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

    private void DeregistrationEvents()
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

    private void InitFighters()
    {
        _player.transform.localPosition = _config.EnemySpawnPoint;
        _player.transform.localRotation = Quaternion.Euler(0f, -90f, 0f);
        _enemy.transform.localPosition = _config.PlayerSpawnPoint;
        _enemy.transform.localRotation = Quaternion.identity;
    }
}
