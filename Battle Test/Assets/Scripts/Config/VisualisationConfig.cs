using UnityEngine;

[CreateAssetMenu]
public class VisualisationConfig : ScriptableObject
{
    [SerializeField] private Material _standard;
    [SerializeField] private Material _selected;    

    public Material Standard => _standard;

    public Material Selected => _selected;    
}
