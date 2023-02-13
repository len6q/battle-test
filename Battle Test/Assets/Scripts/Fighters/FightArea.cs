using System.Collections;
using UnityEngine;
using Zenject;

public sealed class FightArea : MonoBehaviour
{    
    private const float PREPARE_TIME = 1f;    

    private Player _player;
    private Enemy _enemy;        

    public bool IsFightDone { get; private set; }

    [Inject]
    private void Construct(Player player, Enemy enemy)
    {
        _player = player;
        _enemy = enemy;
    }
    
    public IEnumerator Fight()
    {
        IsFightDone = false;
        yield return FightAnimation();
        IsFightDone = true;            
    }
    
    private IEnumerator FightAnimation()
    {
        yield return new WaitForSeconds(PREPARE_TIME);
        for(int i = 0; i < _player.CountParts; i++)
        {          
            if(_player.CanTakeDamage(i))
            {
                _enemy.PlayAttackAnimation();
                yield return _player.TakeDamage(i);
            }
            if(_enemy.CanTakeDamage(i))
            {
                _player.PlayAttackAnimation();
                yield return _enemy.TakeDamage(i);
            }
           
            yield return null;
        }
        yield return new WaitForSeconds(PREPARE_TIME);
    }
}
