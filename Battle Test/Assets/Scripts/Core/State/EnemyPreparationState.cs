using UnityEngine;

public sealed class EnemyPreparationState : GameBaseState
{
    private readonly PreparationHud _preparationHud;
    private readonly GameConfig _config;

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

            _preparationHud.SetValueOnTimer(Mathf.CeilToInt(_preparationTime));
        }
    }

    public EnemyPreparationState(
        IGameStateSwitcher gameStateSwitcher,
        Player player, Enemy enemy,
        GameConfig config, PreparationHud preparationHud)
        : base(gameStateSwitcher, player, enemy)
    {
        _preparationHud = preparationHud;
        _config = config;
    }

    public override void Enter()
    {        
        _preparationHud.Open();
        _preparationTime = _config.PreparationTime;

        _preparationHud.ShowPlayerHealth(_enemy.Health);
        _preparationHud.ShowEnemyHealth(_player.Health);

        InitFighters();
        RegistrationEvents();          
    }

    public override void Exit()
    {
        _preparationHud.Close();
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
        _preparationHud.AddActionOnClick(SwitchState);        

        for (int i = 0; i < _preparationHud.CountAttackFields; i++)
        {
            AttackField attackField = _preparationHud.GetDesiredAttackField(i);
            BodyPart bodyPart = _player.GetDesiredPart(attackField.Type);
            attackField.OnSelected += bodyPart.Select;
            attackField.OnDeselected += bodyPart.Deselect;
            attackField.OnSetAttackValue += bodyPart.SetDamage;
        }

        for (int i = 0; i < _preparationHud.CountProtectionFields; i++)
        {
            ProtectionField protectionField = _preparationHud.GetDesiredProtectionField(i);
            BodyPart bodyPart = _enemy.GetDesiredPart(protectionField.Type);
            protectionField.OnSetProtectionValue += bodyPart.SetProtection;
            protectionField.OnDeselected += bodyPart.Deselect;
        }
    }

    private void DeregistrationEvents()
    {
        _preparationHud.RemoveActionOnClick(SwitchState);        

        for (int i = 0; i < _preparationHud.CountAttackFields; i++)
        {
            AttackField attackField = _preparationHud.GetDesiredAttackField(i);
            BodyPart bodyPart = _player.GetDesiredPart(attackField.Type);
            attackField.OnSelected -= bodyPart.Select;
            attackField.OnDeselected -= bodyPart.Deselect;
            attackField.OnSetAttackValue -= bodyPart.SetDamage;
        }

        for (int i = 0; i < _preparationHud.CountProtectionFields; i++)
        {
            ProtectionField protectionField = _preparationHud.GetDesiredProtectionField(i);
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
