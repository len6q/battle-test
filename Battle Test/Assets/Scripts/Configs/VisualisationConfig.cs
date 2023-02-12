using UnityEngine;

[CreateAssetMenu]
public class VisualisationConfig : ScriptableObject
{
    [SerializeField] private Material _standard;
    [SerializeField] private Material _attack;
    [SerializeField] private Material _protected;

    public Material Standard => _standard;

    public Material Attack => _attack;

    public Material Protected => _protected;
}
