using System;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class AttackField : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] private BodyPartType _type;
    [SerializeField] private TMP_InputField _field;

    public event Action OnSelected;
    public event Action OnDeselected;
    public event Action<int> OnSetAttackValue;
    public event Func<int, bool> CanPushAttackPoint;

    private int _currentValue;

    public BodyPartType Type => _type;

    public void OnDeselect(BaseEventData eventData)
    {        
        OnDeselected?.Invoke();
    }

    public void OnSelect(BaseEventData eventData)
    {
        int.TryParse(_field.text, out _currentValue);
        OnSelected?.Invoke();
    }

    public void ClearField()
    {
        _field.onEndEdit.RemoveAllListeners();
        _field.text = null;
        OnDeselected?.Invoke();
        _field.onEndEdit.AddListener(_ => SetAttackValue(_field));
    }

    private void SetAttackValue(TMP_InputField field)
    {
        if (int.TryParse(field.text, out int value) &&
            value >= 0 &&
            CanPushAttackPoint.Invoke(value - _currentValue))
        {
            OnSetAttackValue?.Invoke(value);            
        }
        else
        {
            field.text = _currentValue.ToString();
        }
    }
}
