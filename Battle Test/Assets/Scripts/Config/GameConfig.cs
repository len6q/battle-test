using UnityEngine;

[CreateAssetMenu]
public class GameConfig : ScriptableObject
{
    [SerializeField] private int _attackPoints = 4;
    [SerializeField] private int _protectionPoints = 4;

    public int AttackPoints => _attackPoints;

    public int ProtectionPoints => _protectionPoints;
}
