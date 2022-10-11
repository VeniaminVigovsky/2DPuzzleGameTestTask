using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SoundSettingsView : MonoBehaviour
{
    public SoundSettingsController SoundSettingsController
    {
        get
        {
            if (!_isInit) Init();
            return _soundSettingsController;
        }
    }

    [SerializeField] private GameObject _soundSettingsPanel;
    [SerializeField] private TMP_Text _gainPercentageTextBox;
    [SerializeField] private GameDataSO _gameDataSO;

    private SoundSettingsController _soundSettingsController;
    private bool _isInit;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (_isInit) return;

        _soundSettingsController = new SoundSettingsController(_soundSettingsPanel, _gainPercentageTextBox, _gameDataSO);

        _isInit = true;
    }

    public void OnButtonClick()
    {
        if (!_isInit) Init();

        _soundSettingsController?.OnButtonClick();
    }
}
