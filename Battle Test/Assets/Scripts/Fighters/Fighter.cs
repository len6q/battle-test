using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Fighter : MonoBehaviour
{
    [SerializeField] protected List<BodyPart> _allParts;    
    
    protected int _health;

    public BodyPart GetDesiredPart(BodyPartType type) => 
        _allParts.FirstOrDefault(part => part.Type == type);    
}

public enum FighterType
{
    Player,
    Enemy
}