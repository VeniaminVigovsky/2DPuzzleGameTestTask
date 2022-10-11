using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameView : MonoBehaviour, ICoroutineHandler
{
    [SerializeField] private GameGoal _gameGoal;
    [SerializeField] private GameBoardView _gameBoardView;
    [SerializeField] private PlayerBoardView _playerBoardView;
    [SerializeField] private SquareBoardView _squareBoardView;

    private GameController _gameController;

    private bool _isInit;

    public void HandleCoroutine(IEnumerator coroutine)
    {
        StopAllCoroutines();
        StartCoroutine(coroutine);
    }

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (_isInit) return;

        _gameController = new GameController(_gameBoardView.BoardController, _playerBoardView.BoardController, _squareBoardView.SquareBoardController, _gameGoal, this);

        _isInit = true;
    }

}
