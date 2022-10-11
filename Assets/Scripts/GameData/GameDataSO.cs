using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName ="SaveSystem/GameData")]
public class GameDataSO : ScriptableObject
{
    public event Action<float> SoundGainSliderValueChanged;

    public List<int> PlayerBoardIndeces
    {
        get
        {
            if (_playerBoardIndeces == null)
                _playerBoardIndeces = new List<int>();

            return _playerBoardIndeces;
        }

        set
        {
            _playerBoardIndeces = value;
        }
    }

    public List<int> GameBoardIndeces
    {
        get
        {
            if (_gameBoardIndeces == null)
                _gameBoardIndeces = new List<int>();

            return _gameBoardIndeces;
        }
        set
        {
            _gameBoardIndeces = value;
        }
    }

    public float SoundGainSliderValue 
    { 
        get => _soundGainSliderValue; 
        set 
        {
            _soundGainSliderValue = value;
            SoundGainSliderValueChanged?.Invoke(SoundGainSliderValue);
        } 
    }

    [SerializeField] private float _soundGainSliderValue;

    private List<int> _playerBoardIndeces, _gameBoardIndeces;
}
