using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    [SerializeField] private List<BodyPart> _allParts;    
    
    private int _health;

    public void Init(int health, Material standard, Material picked)
    {
        _health = health;

        foreach(var part in _allParts)
        {
            part.Init(standard, picked);
        }
    }

    public BodyPart GetDesiredPart(BodyPartType type) => 
        _allParts.FirstOrDefault(part => part.Type == type);    
}
