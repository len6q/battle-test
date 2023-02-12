using System;
using UnityEngine;
using UnityEngine.UI;

public class ProtectionField : MonoBehaviour
{
    [SerializeField] private BodyPartType _type;
    [SerializeField] private Toggle _toggle;    

    public event Action<bool> OnSetProtectionValue;
    public event Func<bool, bool> CanPushProtectionPoints;    

    public BodyPartType Type => _type;    

    public void ClearToggle()
    {
        _toggle.onValueChanged.RemoveAllListeners();
        _toggle.isOn = false;
        _toggle.onValueChanged.AddListener(_ => SetProtectionValue(_toggle));
    }

    public void DisableToggle()
    {
        _toggle.enabled = _toggle.isOn;
    }

    public void EnableToggle()
    {
        _toggle.enabled = true;
    }

    private void SetProtectionValue(Toggle toggle)
    {        
        if (CanPushProtectionPoints.Invoke(toggle.isOn))
        {            
            OnSetProtectionValue?.Invoke(toggle.isOn);
        }                
    }
}
