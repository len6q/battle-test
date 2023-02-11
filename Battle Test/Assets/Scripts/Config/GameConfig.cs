using UnityEngine;

[CreateAssetMenu]
public class GameConfig : ScriptableObject
{
    [SerializeField] private int _attackPoints = 4;
    [SerializeField] private int _protectionPoints = 4;
    [SerializeField] private int _currentHealthPlayer;
    [SerializeField] private int _currentHealthEnemy;

    public int AttackPoints => _attackPoints;

    public int ProtectionPoints => _protectionPoints;

    public int PlayerHealth => _currentHealthPlayer;

    public int EnemyHealth => _currentHealthEnemy;
}
