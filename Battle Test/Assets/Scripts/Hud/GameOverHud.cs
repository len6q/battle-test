using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public sealed class GameOverHud : GameWindow
{
    [SerializeField] private TextMeshProUGUI _gameOverText;
    [SerializeField] private Button _playButton;

    private const string DRAW_MESSAGE = "Ничья";
    private const string PLAYER_WINNER = "Выиграл боец из синего угла";
    private const string ENEMY_WINNER = "Выиграл боец из красного угла";

    public void ShowDraw() =>
        _gameOverText.text = DRAW_MESSAGE;

    public void ShowPlayerWinner() =>
        _gameOverText.text = PLAYER_WINNER;

    public void ShowEnemyWinner() =>
        _gameOverText.text = ENEMY_WINNER;

    public void AddAction(UnityAction action) =>
        _playButton.onClick.AddListener(action);
    
    public void RemoveAction(UnityAction action) =>
        _playButton.onClick.RemoveListener(action);
    
}
