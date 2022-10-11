using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SoundSettingsController
{
    private GameObject _soundSettingsPanel;
    private TMP_Text _gainPercentageTextBox;
    private GameDataSO _gameDataSO;

    private bool _isPanelOpen;
    public SoundSettingsController(GameObject soundSettingsPanel, TMP_Text gainPercentageTextBox, GameDataSO gameDataSO)
    {
        _soundSettingsPanel = soundSettingsPanel;
        _gainPercentageTextBox = gainPercentageTextBox;
        _gameDataSO = gameDataSO;

        if (_gameDataSO != null)
        {
            _gameDataSO.SoundGainSliderValueChanged += OnSliderValueChanged;
            OnSliderValueChanged(_gameDataSO.SoundGainSliderValue);
        }
        _isPanelOpen = false;
    }

    public void OpenPanel(bool open)
    {
        if (_soundSettingsPanel == null) return;

        _soundSettingsPanel.SetActive(open);
    }

    public void OnButtonClick()
    {
        _isPanelOpen = !_isPanelOpen;
        OpenPanel(_isPanelOpen);
    }

    public void OnSliderValueChanged(float value)
    {
        if (_gainPercentageTextBox == null) return;

        var percentageText = value.ToString("p0");
        _gainPercentageTextBox.text = $"Звук: {percentageText}";
    }
}
