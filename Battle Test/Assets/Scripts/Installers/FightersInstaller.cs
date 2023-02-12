using UnityEngine;
using Zenject;

public class FightersInstaller : MonoInstaller
{
    [Header("Player Configs")]
    [SerializeField] private FighterConfig _playerFighterConfig;
    [SerializeField] private VisualisationConfig _playerVisualConfig;
    [SerializeField] private Vector3 _playerSpawnPoint;
    [Space]
    [Header("Enemy Configs")]
    [SerializeField] private FighterConfig _enemyFighterConfig;
    [SerializeField] private VisualisationConfig _enemyVisualConfig;
    [SerializeField] private Vector3 _enemySpawnPoint;

    public override void InstallBindings()
    {
        BindPlayerConfig();
        BindPlayer();

        BindEnemyConfig();
        BindEnemy();
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
            InstantiatePrefabForComponent<Player>(
            _playerFighterConfig.Prefab,
            _playerSpawnPoint,
            Quaternion.identity,
            null);

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
            InstantiatePrefabForComponent<Enemy>(
            _enemyFighterConfig.Prefab,
            _enemySpawnPoint,
            Quaternion.Euler(0f, -90f, 0f),
            null);

        Container.
            BindInstance(fighter).
            AsSingle();
    }
}