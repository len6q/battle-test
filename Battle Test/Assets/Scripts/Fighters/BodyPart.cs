using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class BodyPart : MonoBehaviour
{
    [SerializeField] private BodyPartType _type;

    private MeshRenderer _renderer;
    private VisualisationConfig _visualConfig;
    
    private int _receivedDamage;
    private bool _isProtected;

    public BodyPartType Type => _type;

    public void Init(VisualisationConfig config)
    {
        _visualConfig = config;
        _renderer = GetComponent<MeshRenderer>();
        _renderer.material = _visualConfig.Standard;
    }

    public void Select()
    {
        _renderer.material = _visualConfig.Attack;
    }

    public void Deselect()
    {
        _renderer.material = _visualConfig.Standard;
    }

    public void SetDamage(int value)
    {
        _receivedDamage = value;
    }

    public void SetProtection(bool value)
    {
        _isProtected = value;
        if (_isProtected)
            _renderer.material = _visualConfig.Protected;
        else
            _renderer.material = _visualConfig.Standard;
    }
}
