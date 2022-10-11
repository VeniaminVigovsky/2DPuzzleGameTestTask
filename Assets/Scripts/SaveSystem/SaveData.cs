using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public List<int> PlayerBoardIndeces => _playerBoardIndeces;
    public List<int> GameBoardIndeces => _gameBoardIndeces; 
    public float SoundGainSliderValue => _soundGainSliderValue;

    [SerializeField] private List<int> _playerBoardIndeces, _gameBoardIndeces;

    [SerializeField] private float _soundGainSliderValue;

    public SaveData(List<int> playerBoardIndeces, List<int> gameBoardIndeces, float soundGainSliderValue)
    {
        _playerBoardIndeces = playerBoardIndeces;
        _gameBoardIndeces = gameBoardIndeces;
        _soundGainSliderValue = soundGainSliderValue;
    }

}
