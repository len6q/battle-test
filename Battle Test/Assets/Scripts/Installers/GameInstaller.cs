using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private DefenderHud _defenderHud;
    
    public override void InstallBindings()
    {
        BindConfig();
        BindDefenderHud();
        BindGame();
    }

    private void BindConfig()
    {
        Container.
            BindInstance(_gameConfig);            
    }

    private void BindDefenderHud()
    {
        Container.
            BindInstance(_defenderHud).
            AsSingle();
    }

    private void BindGame()
    {
        Container.
            BindInterfacesAndSelfTo<Game>().
            AsSingle();
    }    
}