using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DefenderHud : MonoBehaviour
{
    [Header("Attack Fields")]
    [SerializeField] private List<AttackField> _allAttackFields;
    [SerializeField] private TextMeshProUGUI _attackPointsText;
    [Space]
    [Header("Protection Fields")]
    [SerializeField] private List<ProtectionField> _allProtectionFields;
    [SerializeField] private TextMeshProUGUI _protectionPointsText;

    private int _attackPoints;
    private int _protectionPoints;    

    public int CountAttackFields => _allAttackFields.Count;

    public int CountProtectionFields => _allProtectionFields.Count;

    private int CurrentAttackPoints
    {
        get => _attackPoints;
        set
        {
            _attackPoints = value;
            _attackPointsText.text = _attackPoints.ToString();
        }
    }

    private int CurrentProtectionPoints
    {
        get => _protectionPoints;
        set
        {
            _protectionPoints = value;
            _protectionPointsText.text = _protectionPoints.ToString();
        }
    }

    private void OnDestroy()
    {
        foreach (var field in _allAttackFields)
        {
            field.CanPushAttackPoint -= CanPushAttackPoint;            
        }
        
        foreach(var field in _allProtectionFields)
        {
            field.CanPushProtectionPoints -= CanPushProtectionPoint;            
        }
    }

    public void Init(int attackPoints, int protectionPoints)
    {        
        CurrentAttackPoints = attackPoints;
        CurrentProtectionPoints = protectionPoints;
        
        foreach(var field in _allAttackFields)
        {
            field.CanPushAttackPoint += CanPushAttackPoint;
        }
        
        foreach (var field in _allProtectionFields)
        {
            field.CanPushProtectionPoints += CanPushProtectionPoint;
        }
    }

    public AttackField GetDesiredAttackField(int indexCollection) => 
        _allAttackFields[indexCollection];

    public ProtectionField GetDesiredProtectionField(int indexCollection) => 
        _allProtectionFields[indexCollection];    

    private bool CanPushAttackPoint(int value)
    {        
        if(CurrentAttackPoints >= value)
        {
            CurrentAttackPoints -= value;
            return true;
        }

        return false;
    }

    private bool CanPushProtectionPoint(bool isOnToggle)
    {                
        if(CurrentProtectionPoints > 0 && isOnToggle)
        {
            CurrentProtectionPoints--;
            if(CurrentProtectionPoints == 0)
            {
                DisableToggles();
            }

            return true;
        }

        if(CurrentProtectionPoints >= 0 && isOnToggle == false)
        {
            CurrentProtectionPoints++;
            if(CurrentProtectionPoints > 0)
            {
                EnableToggles();
            }

            return true;
        }   
        
        return false;
    }

    private void DisableToggles()
    {
        foreach(var field in _allProtectionFields)
        {
            field.Toggle.enabled = field.Toggle.isOn;
        }
    }

    private void EnableToggles()
    {
        foreach(var field in _allProtectionFields)
        {
            field.Toggle.enabled = true;
        }
    }
}
