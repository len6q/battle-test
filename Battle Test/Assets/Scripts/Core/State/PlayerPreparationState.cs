public class PlayerPreparationState : GameBaseState
{
    private const int PREPARATION_TIME = 15;

    public PlayerPreparationState(Player player, Enemy enemy, DefenderHud defenderHud, IGameStateSwitcher gameStateSwitcher)
        : base(player, enemy, defenderHud, gameStateSwitcher)
    {
    }

    public override void Enter()
    {
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

    public override void Exit()
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

        _gameStateSwitcher.SwitchState<EnemyPreparationState>();
    }

    public override void Tick()
    {
        
    }
}
