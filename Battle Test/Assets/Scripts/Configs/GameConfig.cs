using UnityEngine;

[CreateAssetMenu]
public class GameConfig : ScriptableObject
{
    [SerializeField] private float _preparationTime;
    [SerializeField] private int _attackPoints = 4;
    [SerializeField] private int _protectionPoints = 4;
    [SerializeField] private Vector3 _playerSpawnPoint;
    [SerializeField] private Vector3 _enemySpawnPoint;

    public float PreparationTime => _preparationTime;

    public int AttackPoints => _attackPoints;

    public int ProtectionPoints => _protectionPoints;

    public Vector3 PlayerSpawnPoint => _playerSpawnPoint;

    public Vector3 EnemySpawnPoint => _enemySpawnPoint;
}
