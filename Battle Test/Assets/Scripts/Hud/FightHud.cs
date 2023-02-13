using TMPro;
using UnityEngine;

public sealed class FightHud : GameWindow
{
    [SerializeField] private TextMeshProUGUI _playerHealthText;
    [SerializeField] private TextMeshProUGUI _enemyHealthText;

    public void ShowPlayerHealth(int health) =>
        _playerHealthText.text = health.ToString();    

    public void ShowEnemyHealth(int health) =>
        _enemyHealthText.text = health.ToString();

}
