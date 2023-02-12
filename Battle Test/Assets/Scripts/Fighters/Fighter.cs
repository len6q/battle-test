using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public abstract class Fighter : MonoBehaviour
{
    [SerializeField] private List<BodyPart> _allParts;    
    
    private int _health;

    [Inject]
    private void Construct(FighterConfig fighterConfig, VisualisationConfig visualConfig)
    {
        _health = fighterConfig.Health;

        foreach (var part in _allParts)
        {
            part.Init(visualConfig.Standard, visualConfig.Selected);
        }
    }

    public BodyPart GetDesiredPart(BodyPartType type) => 
        _allParts.FirstOrDefault(part => part.Type == type);    
}
