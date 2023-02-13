using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public sealed class PreparationHud : GameWindow
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
    [Space]
    [SerializeField] private TextMeshProUGUI _playerHealthText;
    [SerializeField] private TextMeshProUGUI _enemyHealthText;

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
    }

    public override void Open()
    {
        _allAttackFields.ForEach(field => field.CanPushAttackPoint += CanPushAttackPoint);
        _allProtectionFields.ForEach(field => field.CanPushProtectionPoints += CanPushProtectionPoint);        

        base.Open();
    }

    public override void Close()
    {
        Refresh();

        _allAttackFields.ForEach(field => field.CanPushAttackPoint -= CanPushAttackPoint);
        _allProtectionFields.ForEach(field => field.CanPushProtectionPoints -= CanPushProtectionPoint);        

        base.Close();
    }

    public void AddActionOnClick(UnityAction action) =>
        _playButton.onClick.AddListener(action);

    public void RemoveActionOnClick(UnityAction action) =>
        _playButton.onClick.RemoveListener(action);

    public void ShowPlayerHealth(int health) =>
        _playerHealthText.text = health.ToString();

    public void ShowEnemyHealth(int health) =>
        _enemyHealthText.text = health.ToString();

    public AttackField GetDesiredAttackField(int indexCollection) => 
        _allAttackFields[indexCollection];

    public ProtectionField GetDesiredProtectionField(int indexCollection) => 
        _allProtectionFields[indexCollection];

    public void SetValueOnTimer(int value) =>
        _timer.text = value.ToString();

    private void Refresh()
    {
        CurrentAttackPoints = _gameConfig.AttackPoints;
        CurrentProtectionPoints = _gameConfig.ProtectionPoints;

        _allAttackFields.ForEach(field => field.ClearField());
        _allProtectionFields.ForEach(field => field.ClearToggle());
    }

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
