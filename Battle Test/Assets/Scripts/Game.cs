using UnityEngine;

public class Game : MonoBehaviour
{        
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private VisualisationConfig _visualConfig;
    [SerializeField] private DefenderHud _defenderHud;
    [SerializeField] private Fighter _player;
    [SerializeField] private Fighter _enemy;
    
    private void Start()
    {
        _player.Init(
            _gameConfig.PlayerHealth,
            _visualConfig.Standard,
            _visualConfig.Selected);
        
        _enemy.Init(
            _gameConfig.EnemyHealth,
            _visualConfig.Standard,
            _visualConfig.Protected);

        _defenderHud.Init(
            _gameConfig.AttackPoints,
            _gameConfig.ProtectionPoints);
    }

    private void OnEnable()
    {
        for (int i = 0; i < _defenderHud.CountAttackFields; i++)
        {
            AttackField attackField = _defenderHud.GetDesiredAttackField(i);
            BodyPart bodyPart = _player.GetDesiredPart(attackField.Type);
            attackField.OnSelected += bodyPart.Select;
            attackField.OnDeselected += bodyPart.Deselect;
            attackField.OnSetAttackValue += bodyPart.SetDamage;
        }

        for(int i = 0; i < _defenderHud.CountProtectionFields; i++)
        {
            ProtectionField protectionField = _defenderHud.GetDesiredProtectionField(i);
            BodyPart bodyPart = _enemy.GetDesiredPart(protectionField.Type);
            protectionField.OnSetProtectionValue += bodyPart.SetProtection;
        }
    }

    private void OnDisable()
    {
        for(int i = 0; i < _defenderHud.CountAttackFields; i++)
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
}
