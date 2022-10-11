using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanelView : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    [SerializeField] private Image _loadingBar;

    [SerializeField] private GameObject _startButton;

    private StartPanelController _startPanelController;
    private bool _isInit;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (_isInit) return;
        _startPanelController = new StartPanelController(_panel, _loadingBar, _startButton);
    }

    public void OnStartButtonClicked()
    {
        if (!_isInit) Init();

        _startPanelController?.OnStartButtonClicked();
    }
}
