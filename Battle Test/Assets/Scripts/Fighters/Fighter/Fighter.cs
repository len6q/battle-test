using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public abstract class Fighter : MonoBehaviour
{
    [SerializeField] private List<BodyPart> _allParts;

    public event Action<int> OnTakenDamage;

    private const float WAIT_TIME = 1f;
    private int _health;
    private FighterConfig _fighterConfig;

    public bool IsDead => _health <= 0;

    public int CountParts => _allParts.Count;

    public int Health => _health;

    public bool Visible
    {
        set => gameObject.SetActive(value);
    }

    [Inject]
    private void Construct(FighterConfig fighterConfig, VisualisationConfig visualConfig)
    {
        _fighterConfig = fighterConfig;        
        _allParts.ForEach(part => part.Init(visualConfig));
    }

    public void SetHealth() =>
        _health = _fighterConfig.Health;
    
    public BodyPart GetDesiredPart(BodyPartType type) => 
        _allParts.FirstOrDefault(part => part.Type == type);    

    public void Refresh() =>
        _allParts.ForEach(part => part.Clear());
            
    public IEnumerator TakeDamage(int index)
    {
        if(_allParts[index].IsProtected || 
            _allParts[index].ReceivedDamage == 0)        
            yield break;

        yield return new WaitForSeconds(WAIT_TIME);
        _health -= _allParts[index].ReceivedDamage;
        OnTakenDamage?.Invoke(_health);
    }
}
