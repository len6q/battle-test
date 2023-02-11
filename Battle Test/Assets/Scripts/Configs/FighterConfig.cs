using UnityEngine;

[CreateAssetMenu]
public class FighterConfig : ScriptableObject
{
    [SerializeField] private Fighter _fighterPrefab;
    [SerializeField] private int _health;

    public Fighter Prefab => _fighterPrefab;

    public int Health => _health;
}
