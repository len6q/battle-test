using Zenject;

public sealed class Enemy : Fighter 
{    
    [Inject]
    private void Construct(
        [Inject(Id = FighterType.Enemy)] FighterConfig fighterConfig,
        [Inject(Id = FighterType.Enemy)] VisualisationConfig visualConfig)
    {
        _health = fighterConfig.Health;

        foreach (var part in _allParts)
        {
            part.Init(visualConfig.Standard, visualConfig.Selected);
        }
    }
}
