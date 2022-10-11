using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManagerController
{
    private AudioMixer _audioMixer;
    private string _masterGainParamName;

    private GameDataSO _gameDataSO;    
    private AudioSource _audioSource;

    private ICoroutineHandler _coroutineHandler;

    public AudioManagerController(AudioMixer audioMixer, string soundChannelName, GameDataSO gameDataSO, AudioSource audioSource, ICoroutineHandler coroutineHandler)
    {
        _audioMixer = audioMixer;
        _masterGainParamName = soundChannelName;

        _gameDataSO = gameDataSO;
        _audioSource = audioSource;        
        _coroutineHandler = coroutineHandler;

        if (_gameDataSO != null)
        {
            _gameDataSO.SoundGainSliderValueChanged += OnGainSliderValueChanged;
            _coroutineHandler?.HandleCoroutine(InitAudio(_gameDataSO.SoundGainSliderValue));
        }

    }

    private void OnGainSliderValueChanged(float value)
    {
        if (_audioMixer == null) return;

        _audioMixer.SetFloat(_masterGainParamName, Mathf.Log10(value) * 20);
    }

    private IEnumerator InitAudio(float gain)
    {
        yield return new WaitForSeconds(1.0f);
        OnGainSliderValueChanged(_gameDataSO.SoundGainSliderValue);
        yield return null;
        _audioSource?.Play();
    }
}
