using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GainSliderController 
{
    private GameDataSO _gameDataSO;
    private Slider _slider;

    public GainSliderController(GameDataSO gameDataSO, Slider slider)
    {
        _gameDataSO = gameDataSO;
        _slider = slider;

        _slider.value = Mathf.Max(_gameDataSO.SoundGainSliderValue, _slider.minValue);
    }

    public void ChangeGain(float value)
    {
        _gameDataSO.SoundGainSliderValue = value;
    }

}
