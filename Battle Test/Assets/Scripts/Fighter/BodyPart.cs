using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class BodyPart : MonoBehaviour
{
    [SerializeField] private BodyPartType _type;

    private Material _standard;
    private Material _selected;    
    private MeshRenderer _renderer;
    
    private int _receivedDamage;
    private bool _isProtected;

    public BodyPartType Type => _type;

    public void Init(Material standard, Material selected)
    {
        _standard = standard;
        _selected = selected;

        _renderer = GetComponent<MeshRenderer>();
        _renderer.material = _standard;
    }

    public void Select()
    {
        _renderer.material = _selected;
    }

    public void Deselect()
    {
        _renderer.material = _standard;
    }

    public void SetDamage(int value)
    {
        _receivedDamage = value;
    }

    public void SetProtection(bool value)
    {
        _isProtected = value;
        if (_isProtected)
            _renderer.material = _selected;
        else
            _renderer.material = _standard;
    }
}
