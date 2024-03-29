﻿using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public sealed class BodyPart : MonoBehaviour
{
    [SerializeField] private BodyPartType _type;

    private MeshRenderer _renderer;
    private VisualisationConfig _visualConfig;
    
    private int _receivedDamage;
    private bool _isProtected;

    public BodyPartType Type => _type;

    public int ReceivedDamage => _receivedDamage;

    public bool IsProtected => _isProtected;

    public void Init(VisualisationConfig config)
    {
        _visualConfig = config;
        _renderer = GetComponent<MeshRenderer>();
        _renderer.material = _visualConfig.Standard;
    }

    public void Clear()
    {
        _isProtected = false;
        _receivedDamage = 0;
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
