using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveHandlerView : MonoBehaviour
{
    public SaveHandlerController SaveHandlerController
    {
        get
        {
            if (!_isInit) Init();
            return _saveHandlerController;
        }
    }

    [SerializeField] private GameDataSO _gameDataSO;

    private SaveHandlerController _saveHandlerController;

    private bool _isInit;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (_isInit) return;

        _saveHandlerController = new SaveHandlerController(_gameDataSO);

        _isInit = true;
    }

    private void OnApplicationQuit()
    {
        if (!_isInit) Init();

        _saveHandlerController?.Save();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!_isInit) Init();

        if (!focus)
            _saveHandlerController?.Save();
    }

    private void OnApplicationPause(bool pause)
    {
        if (!_isInit) Init();

        if (pause)
            _saveHandlerController?.Save();
    }

}
