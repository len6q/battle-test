using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ProtectionField : MonoBehaviour
{
    [SerializeField] private BodyPartType _type;

    public event Action<bool> OnSetProtectionValue;
    public event Func<bool, bool> CanPushProtectionPoints;    

    private Toggle _toggle;    

    public BodyPartType Type => _type;

    public Toggle Toggle => _toggle;

    private void Start()
    {
        _toggle = GetComponent<Toggle>();
        _toggle.onValueChanged.AddListener(_ => SetProtectionValue(_toggle));
    }

    private void OnDestroy()
    {
        _toggle.onValueChanged.RemoveListener(_ => SetProtectionValue(_toggle));
    }

    //TODO Исправить проблему с onvaluechanged и ison toggle//
    private void SetProtectionValue(Toggle toggle)
    {        
        if (CanPushProtectionPoints.Invoke(toggle.isOn))
        {            
            OnSetProtectionValue?.Invoke(toggle.isOn);
        }                
    }
}
