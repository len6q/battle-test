using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private GameConfig _gameConfig;
    
    public override void InstallBindings()
    {
        BindConfig();        
        BindGame();
    }

    private void BindConfig()
    {
        Container.
            BindInstance(_gameConfig);            
    }

    private void BindGame()
    {
        Container.
            BindInterfacesAndSelfTo<Game>().
            AsSingle();
    }    
}