using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GainSliderView : MonoBehaviour
{
    [SerializeField] private Slider _gainSlider;
    [SerializeField] private GameDataSO _gameDataSO;

    private GainSliderController _gainSliderController;

    private bool _isInit;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (_isInit) return;

        _gainSliderController = new GainSliderController(_gameDataSO, _gainSlider);

        _isInit = true;
    }

    public void OnSliderValueChanged(float value)
    {
        if (!_isInit) Init();

        _gainSliderController?.ChangeGain(value);
    }
}
