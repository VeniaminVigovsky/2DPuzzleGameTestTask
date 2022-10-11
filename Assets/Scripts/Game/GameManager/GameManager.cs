using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, ICoroutineHandler
{   

    [SerializeField] private int _gameSceneBuildIndex;
    [SerializeField] private AudioManagerView _audioManagerView;

    private SaveHandlerController _saveHandlerController;

    private GameManagerController _gameManagerController;    

    private bool _isInit;
    private void Awake()
    {
        Init();
    }    

    private void Init()
    {
        if (_isInit) return;

        var saveHandlerView = GetComponent<SaveHandlerView>();
        if (saveHandlerView != null)
            _saveHandlerController = saveHandlerView.SaveHandlerController;

        _saveHandlerController?.Load();

        _audioManagerView?.Init();

        _gameManagerController = new GameManagerController(_gameSceneBuildIndex, this);

        _isInit = true;
    }

    public void HandleCoroutine(IEnumerator coroutine)
    {
        StopAllCoroutines();
        StartCoroutine(coroutine);
    }

}
