﻿using System;
using System.Collections;
using UnityEngine;
using Zenject;

public sealed class FightArea : MonoBehaviour
{
    public event Action<bool> OnFight;

    private Player _player;
    private Enemy _enemy;        

    [Inject]
    private void Construct(Player player, Enemy enemy)
    {
        _player = player;
        _enemy = enemy;
    }
    
    public IEnumerator Fight()
    {
        yield return FightAnimation();
        OnFight?.Invoke(_player.IsDead || _enemy.IsDead);             
    }
    
    private IEnumerator FightAnimation()
    {
        for(int i = 0; i < _player.CountParts; i++)
        {            
            yield return _player.TakeDamage(i);
            yield return _enemy.TakeDamage(i);
            yield return null;
        }
    }
}
