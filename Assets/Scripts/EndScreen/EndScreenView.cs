using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreenView : MonoBehaviour
{
    [SerializeField] private GameObject _endScreenPanel;
    [SerializeField] private TMP_Text _messageTextBox;
    [SerializeField] private string _victoryText, _loseText;

    private EndScreenController _endScreenController;

    private bool _isInit;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (_isInit) return;

        _endScreenController = new EndScreenController(_endScreenPanel, _messageTextBox, _victoryText, _loseText);

        _isInit = true;
    }

    public void OnRestartButtonClicked()
    {
        if (!_isInit) Init();

        _endScreenController?.OnRestartButtonPressed();
    }

}
