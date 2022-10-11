using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreenController
{
    private GameObject _endScreenPanel;
    private TMP_Text _messageTextBlock;
    private string _victoryText, _loseText;

    public EndScreenController(GameObject endScreenPanel, TMP_Text messageTextBlock, string victoryText, string loseText)
    {
        _endScreenPanel = endScreenPanel;
        _messageTextBlock = messageTextBlock;
        _victoryText = victoryText;
        _loseText = loseText;

        GameStateHandler.GameStateChanged += OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState gameState)
    {
        if (_messageTextBlock == null || _endScreenPanel == null) return;

        switch (gameState)
        {
            case GameState.Win:
                _messageTextBlock.text = _victoryText;
                EnableEndScreenPanel(true);
                break;
            case GameState.Lose:
                _messageTextBlock.text = _loseText;
                EnableEndScreenPanel(true);
                break;
            case GameState.Restart:
                EnableEndScreenPanel(false);
                GameStateHandler.GameStateChanged -= OnGameStateChanged;
                break;

        }
    }

    public void OnRestartButtonPressed()
    {
        GameStateHandler.ChangeGameState(GameState.Restart);
    }

    private void EnableEndScreenPanel(bool enabled)
    {
        if (_endScreenPanel == null) return;

        _endScreenPanel.SetActive(enabled);
    }
}
