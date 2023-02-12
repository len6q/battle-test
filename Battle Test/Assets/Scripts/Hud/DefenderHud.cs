using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class DefenderHud : MonoBehaviour
{
    [Header("Attack Fields")]
    [SerializeField] private List<AttackField> _allAttackFields;
    [SerializeField] private TextMeshProUGUI _attackPointsText;
    [Space]
    [Header("Protection Fields")]
    [SerializeField] private List<ProtectionField> _allProtectionFields;
    [SerializeField] private TextMeshProUGUI _protectionPointsText;
    [Space]
    [SerializeField] private Button _playButton;
    [SerializeField] private TextMeshProUGUI _timer;

    public event Action OnClickPlayButton;

    private GameConfig _gameConfig;
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

            _playButton.gameObject.SetActive(
                _attackPoints == 0 && _protectionPoints == 0);            
        }
    }

    private int CurrentProtectionPoints
    {
        get => _protectionPoints;
        set
        {
            _protectionPoints = value;
            _protectionPointsText.text = _protectionPoints.ToString();

            _playButton.gameObject.SetActive(
                _attackPoints == 0 && _protectionPoints == 0);
        }
    }
    
    [Inject]
    private void Construct(GameConfig config)
    {
        _gameConfig = config;
        Refresh();

        _allAttackFields.ForEach(field => field.CanPushAttackPoint += CanPushAttackPoint);
        _allProtectionFields.ForEach(field => field.CanPushProtectionPoints += CanPushProtectionPoint);

        _playButton.onClick.AddListener(ClickButton);
    }

    private void OnDestroy()
    {
        _allAttackFields.ForEach(field => field.CanPushAttackPoint -= CanPushAttackPoint);
        _allProtectionFields.ForEach(field => field.CanPushProtectionPoints -= CanPushProtectionPoint);

        _playButton.onClick.RemoveListener(ClickButton);
    }

    public void Refresh()
    {
        CurrentAttackPoints = _gameConfig.AttackPoints;
        CurrentProtectionPoints = _gameConfig.ProtectionPoints;

        _allAttackFields.ForEach(field => field.ClearField());
        _allProtectionFields.ForEach(field => field.ClearToggle());
    }

    public AttackField GetDesiredAttackField(int indexCollection) => 
        _allAttackFields[indexCollection];

    public ProtectionField GetDesiredProtectionField(int indexCollection) => 
        _allProtectionFields[indexCollection];

    public void SetValueOnTimer(int value) =>
        _timer.text = value.ToString();

    private void ClickButton() =>
        OnClickPlayButton?.Invoke();
    
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

    private void DisableToggles() =>
        _allProtectionFields.ForEach(field => field.DisableToggle());

    private void EnableToggles() =>
        _allProtectionFields.ForEach(field => field.EnableToggle());
}
