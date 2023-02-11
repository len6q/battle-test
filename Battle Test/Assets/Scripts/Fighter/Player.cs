using Zenject;

public sealed class Player : Fighter 
{    
    [Inject]
    private void Construct(
        [Inject(Id = FighterType.Player)] FighterConfig fighterConfig,
        [Inject(Id = FighterType.Player)] VisualisationConfig visualConfig)
    {
        _health = fighterConfig.Health;

        foreach (var part in _allParts)
        {
            part.Init(visualConfig.Standard, visualConfig.Selected);
        }
    }

}
