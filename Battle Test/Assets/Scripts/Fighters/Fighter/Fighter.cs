using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public abstract class Fighter : MonoBehaviour
{
    [SerializeField] private List<BodyPart> _allParts;
    [SerializeField] private Animator _animator;

    public event Action<int> OnTakenDamage;

    private int _health;
    private FighterConfig _fighterConfig;

    private const float WAIT_TIME = 1f;
    private const string PROTECT_TRIGGER = "Protect";
    private const string TAKE_DAMAGE_TRIGGER = "TakeDamage";
    private const string ATTACK_TRIGGER = "Attack";

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
        if (_allParts[index].IsProtected)
        {
            _animator.SetTrigger(PROTECT_TRIGGER);
            yield return new WaitForSeconds(WAIT_TIME);
            yield break;
        }

        _animator.SetTrigger(TAKE_DAMAGE_TRIGGER);
        _health -= _allParts[index].ReceivedDamage;
        OnTakenDamage?.Invoke(_health);
        yield return new WaitForSeconds(WAIT_TIME);        
    }

    public bool CanTakeDamage(int index) =>
        _allParts[index].ReceivedDamage != 0;

    public void PlayAttackAnimation() =>
        _animator.SetTrigger(ATTACK_TRIGGER);
}
