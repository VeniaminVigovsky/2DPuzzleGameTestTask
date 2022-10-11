using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class GameStateHandler
{
    public static GameState GAMESTATE => _gameState;

    public static event Action<GameState> GameStateChanged;

    private static GameState _gameState;

    public static void ChangeGameState(GameState gameState)
    {
        if (_gameState == gameState) return;

        _gameState = gameState;
        GameStateChanged?.Invoke(gameState);
    }
}

public enum GameState
{
    LoadGame,
    Ready,
    Start,
    Win,
    Lose,
    Restart,
    Restarted
     
}
