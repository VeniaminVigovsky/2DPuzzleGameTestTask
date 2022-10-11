using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManagerView : MonoBehaviour, ICoroutineHandler
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private string _masterGainParamName;
    [SerializeField] private GameDataSO _gameDataSO;

    private AudioManagerController _audioManagerController;

    private bool _isInit;

    public void HandleCoroutine(IEnumerator coroutine)
    {
        StopAllCoroutines();
        StartCoroutine(coroutine);
    }

    public void Init()
    {
        if (_isInit) return;

        var audioSource = GetComponent<AudioSource>();

        _audioManagerController = new AudioManagerController(_audioMixer, _masterGainParamName, _gameDataSO, audioSource, this);

        _isInit = true;
    }

}
