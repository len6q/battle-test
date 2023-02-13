using UnityEngine;
using Zenject;

public class FightersInstaller : MonoInstaller
{
    [SerializeField] private FightArea _fightAreaPrefab;
    [Space]
    [Header("Player Configs")]
    [SerializeField] private FighterConfig _playerFighterConfig;
    [SerializeField] private VisualisationConfig _playerVisualConfig;    
    [Space]
    [Header("Enemy Configs")]
    [SerializeField] private FighterConfig _enemyFighterConfig;
    [SerializeField] private VisualisationConfig _enemyVisualConfig;    

    public override void InstallBindings()
    {
        BindPlayerConfig();
        BindPlayer();

        BindEnemyConfig();
        BindEnemy();

        BindFightArea();
    }

    private void BindPlayerConfig()
    {
        Container.
            BindInstance(_playerFighterConfig).
            WhenInjectedInto<Player>();
        
        Container.
            BindInstance(_playerVisualConfig).
            WhenInjectedInto<Player>();
    }

    private void BindPlayer()
    {
        var fighter = Container.
            InstantiatePrefabForComponent<Player>(_playerFighterConfig.Prefab);

        Container.
            BindInstance(fighter).
            AsSingle();
    }

    private void BindEnemyConfig()
    {
        Container.
            BindInstance(_enemyFighterConfig).
            WhenInjectedInto<Enemy>();
        
        Container.
            BindInstance(_enemyVisualConfig).
            WhenInjectedInto<Enemy>();
    }

    private void BindEnemy()
    {
        var fighter = Container.
            InstantiatePrefabForComponent<Enemy>(_enemyFighterConfig.Prefab);

        Container.
            BindInstance(fighter).
            AsSingle();
    }

    private void BindFightArea()
    {
        var area = Container.
            InstantiatePrefabForComponent<FightArea>(_fightAreaPrefab);

        Container.
            BindInstance(area).
            AsSingle();
    }
}