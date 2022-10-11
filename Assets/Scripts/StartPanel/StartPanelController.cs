using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanelController
{
    private GameObject _panel;

    private Image _loadingBar;

    private GameObject _startButton;

    public StartPanelController(GameObject panel, Image loadingBar, GameObject startButton)
    {
        _panel = panel;
        _loadingBar = loadingBar;
        _startButton = startButton;

        GameStateHandler.GameStateChanged += OnGameStateChanged;
    }

    public void OnStartButtonClicked()
    {
        GameStateHandler.ChangeGameState(GameState.Start);
    }

    private void OnGameStateChanged(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.LoadGame:
                EnterLoadingState();
                break;
            case GameState.Ready:
                EnterReadyState();
                break;
            case GameState.Start:
                EnterStartState();
                break;
            case GameState.Restarted:
                EnterStartState();                 
                break;
            default:
                break;
        }
    }

    private void EnterStartState()
    {
        if (_panel == null) return;
        _panel.SetActive(false);
    }

    private void EnterLoadingState()
    {
        if (_panel == null || _loadingBar == null || _startButton == null) return;

        _panel.SetActive(true);
        _loadingBar.gameObject.SetActive(true);
        GameManagerController.LoadingProgressChanged += OnLoadingProgressChanged;
        _startButton.SetActive(false);
    }

    private void EnterReadyState()
    {
        if (_panel == null || _loadingBar == null || _startButton == null) return;

        _panel.SetActive(true);
        _loadingBar.gameObject.SetActive(false);
        GameManagerController.LoadingProgressChanged -= OnLoadingProgressChanged;
        _startButton.SetActive(true);

    }

    private void OnLoadingProgressChanged(float progress)
    {
        if (_loadingBar == null) return;

        _loadingBar.fillAmount = progress;
    }
 
}
