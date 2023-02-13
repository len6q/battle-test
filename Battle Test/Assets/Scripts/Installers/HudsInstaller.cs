using UnityEngine;
using Zenject;

public class HudsInstaller : MonoInstaller
{
    [SerializeField] private PreparationHud _preparationHud;
    [SerializeField] private FightHud _fightHud;
    [SerializeField] private GameOverHud _gameOverHud;

    public override void InstallBindings()
    {
        BindPreparationHud();
        BindFightHud();
        BindGameOverHud();
    }

    private void BindPreparationHud()
    {
        Container.
            BindInstance(_preparationHud).
            AsSingle();
    }

    private void BindFightHud()
    {
        Container.
            BindInstance(_fightHud).
            AsSingle();
    }

    private void BindGameOverHud()
    {
        Container.
            BindInstance(_gameOverHud).
            AsSingle();
    }
}