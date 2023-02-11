using UnityEngine;

[CreateAssetMenu]
public class VisualisationConfig : ScriptableObject
{
    [SerializeField] private Material _standardMat;
    [SerializeField] private Material _selectedMat;
    [SerializeField] private Material _protectedMat;

    public Material Standard => _standardMat;

    public Material Selected => _selectedMat;

    public Material Protected => _protectedMat;
}
