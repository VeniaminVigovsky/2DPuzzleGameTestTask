using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManagerController
{
    public static event Action<float> LoadingProgressChanged;

    private int _gameSceneIndex;

    private ICoroutineHandler _coroutineHandler;

    public GameManagerController(int gameSceneIndex, ICoroutineHandler coroutineHandler)
    {
        _gameSceneIndex = gameSceneIndex;
        _coroutineHandler = coroutineHandler;
        _coroutineHandler?.HandleCoroutine(LoadGameScene());
        GameStateHandler.GameStateChanged += OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState gameState)
    {
        if (gameState == GameState.Restart)
        {
            _coroutineHandler?.HandleCoroutine(ReloadGameScene());
        }
    }

    private IEnumerator LoadGameScene()
    {
        GameStateHandler.ChangeGameState(GameState.LoadGame);

        LoadingProgressChanged?.Invoke(0.0f);

        var loadOperation = SceneManager.LoadSceneAsync(_gameSceneIndex, LoadSceneMode.Additive);

        while (!loadOperation.isDone)
        {
            var progress = Mathf.Lerp(0.0f, 0.9f, loadOperation.progress);
            LoadingProgressChanged?.Invoke(progress);
            yield return null; 
        }

        GameStateHandler.ChangeGameState(GameState.Ready);
    }

    private IEnumerator ReloadGameScene()
    {
        GameStateHandler.ChangeGameState(GameState.LoadGame);

        var unloadOperation = SceneManager.UnloadSceneAsync(_gameSceneIndex);

        while (!unloadOperation.isDone)
        {
            var progress = Mathf.Lerp(0.0f, 0.9f, unloadOperation.progress);
            LoadingProgressChanged?.Invoke(progress);
            yield return null; 
        }

        LoadingProgressChanged?.Invoke(0.0f);

        var loadOperation = SceneManager.LoadSceneAsync(_gameSceneIndex, LoadSceneMode.Additive);

        while (!loadOperation.isDone)
        {
            var progress = Mathf.Lerp(0.0f, 0.9f, loadOperation.progress);
            LoadingProgressChanged?.Invoke(progress);
            yield return null;
        }

        GameStateHandler.ChangeGameState(GameState.Restarted);
    }
}
