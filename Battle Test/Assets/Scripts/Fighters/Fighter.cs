using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public abstract class Fighter : MonoBehaviour
{
    [SerializeField] private List<BodyPart> _allParts;

    private const float WAIT_TIME = 1f;

    private int _health;

    public bool IsDead => _health <= 0;

    public int CountParts => _allParts.Count;

    [Inject]
    private void Construct(FighterConfig fighterConfig, VisualisationConfig visualConfig)
    {
        _health = fighterConfig.Health;
        _allParts.ForEach(part => part.Init(visualConfig));
    }

    public BodyPart GetDesiredPart(BodyPartType type) => 
        _allParts.FirstOrDefault(part => part.Type == type);    

    public IEnumerator TakeDamage(int index)
    {
        if(_allParts[index].IsProtected || 
            _allParts[index].ReceivedDamage == 0)        
            yield break;

        yield return new WaitForSeconds(WAIT_TIME);
        _health -= _allParts[index].ReceivedDamage;
        Debug.Log($"{name} is taking damage!");
    }
}
