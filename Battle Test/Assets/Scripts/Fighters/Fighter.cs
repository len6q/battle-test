using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public abstract class Fighter : MonoBehaviour
{
    [SerializeField] private List<BodyPart> _allParts;    
    
    private int _health;

    public int Health => _health;

    [Inject]
    private void Construct(FighterConfig fighterConfig, VisualisationConfig visualConfig)
    {
        _health = fighterConfig.Health;
        _allParts.ForEach(part => part.Init(visualConfig));
    }

    public BodyPart GetDesiredPart(BodyPartType type) => 
        _allParts.FirstOrDefault(part => part.Type == type);    

    public void Fight()
    {
        foreach (var part in _allParts)
        {
            if(part.IsProtected == false)
            {
                _health -= part.ReceivedDamage;
            }
        }
        Debug.Log(_health);
    }
}
