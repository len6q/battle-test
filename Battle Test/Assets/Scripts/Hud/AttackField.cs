using System;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

[RequireComponent(typeof(TMP_InputField))]
public class AttackField : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] private BodyPartType _type;

    public event Action OnSelected;
    public event Action OnDeselected;
    public event Action<int> OnSetAttackValue;
    public event Func<int, bool> CanPushAttackPoint;

    private TMP_InputField _field;
    private int _currentValue;

    public BodyPartType Type => _type;

    private void Start()
    {
        _field = GetComponent<TMP_InputField>();
        _field.onEndEdit.AddListener(_ => SetAttackValue(_field));
    }

    private void OnDestroy()
    {
        _field.onEndEdit.RemoveListener(_ => SetAttackValue(_field));
    }

    public void OnDeselect(BaseEventData eventData)
    {
        OnDeselected?.Invoke();
    }

    public void OnSelect(BaseEventData eventData)
    {
        int.TryParse(_field.text, out _currentValue);
        OnSelected?.Invoke();
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
