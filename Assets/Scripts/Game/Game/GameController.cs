using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : IGameDataSaver
{
    private IBoardController _gameBoardController, _playerBoardController;
    private ISquareBoardController _squareBoardController;
    private GameGoal _gameGoal;
    private GameDataSO _gameDataSO;

    private ICoroutineHandler _coroutineHandler;

    private Dictionary<ICellController, bool> _occupiedGoalCells;
    private Dictionary<ISquareController, IBoardController> _squareOnBoards;

    public GameController(IBoardController gameBoardController, IBoardController playerBoardController, ISquareBoardController squareBoardController, GameGoal gameGoal, ICoroutineHandler coroutineHandler)
    {
        _gameBoardController = gameBoardController;
        _playerBoardController = playerBoardController;
        _squareBoardController = squareBoardController;
        _gameGoal = gameGoal;
        _coroutineHandler = coroutineHandler;

        _occupiedGoalCells = new Dictionary<ICellController, bool>();
        _squareOnBoards = new Dictionary<ISquareController, IBoardController>();

        var cells = _gameBoardController.Cells;
        var goalIndeces = _gameGoal.GoalIndeces;
        if (cells != null && goalIndeces != null)
        {
            var cellCount = cells.Length;

            if (goalIndeces != null)
            {
                foreach (var goalIndex in goalIndeces)
                {
                    if (goalIndex > 0 && goalIndex <= cellCount)
                    {
                        var cell = cells[goalIndex - 1];
                        _occupiedGoalCells[cell] = false;
                    }
                }
            }
        }

        _gameBoardController.SquareAssigned += OnSquareAssigned;
        _playerBoardController.SquareAssigned += OnSquareAssigned;

        RegisterGameDataSaver();
        InitSquares();
    }

    private void InitSquares()
    {
        if (_gameGoal == null || _squareBoardController == null ||
            _gameBoardController == null || _playerBoardController == null) return;

        var goalIndeces = _gameGoal.GoalIndeces;
        var startIndeces = _gameGoal.StartIndeces;        

        if (goalIndeces == null) return;

        var playerBoardCellIndex = 0;

        if (_gameDataSO != null)
        {
            var playerBoardIndeces = _gameDataSO.PlayerBoardIndeces;
            var gameBoardIndeces = _gameDataSO.GameBoardIndeces;

            if (playerBoardIndeces.Count > 0)
            {
                foreach (var playerBoardIndex in playerBoardIndeces)
                {
                    _squareBoardController.PutSquareOnBoard(_playerBoardController, playerBoardIndex);
                }
            }
            else
            {
                foreach (var goalIndex in goalIndeces)
                {
                    if (startIndeces == null || !startIndeces.Contains(goalIndex))
                        _squareBoardController.PutSquareOnBoard(_playerBoardController, playerBoardCellIndex++);
                }
            }

            foreach (var gameBoardIndex in gameBoardIndeces)
            {
                if (startIndeces != null && startIndeces.Contains(gameBoardIndex + 1)) continue;

                _squareBoardController.PutSquareOnBoard(_gameBoardController, gameBoardIndex);
            }
        }
        else
        {
            foreach (var goalIndex in goalIndeces)
            {
                if (startIndeces == null || !startIndeces.Contains(goalIndex))
                    _squareBoardController.PutSquareOnBoard(_playerBoardController, playerBoardCellIndex++);
            }
        }

        if (startIndeces == null) return;

        foreach (var startIndex in startIndeces)
        {
            if (!goalIndeces.Contains(startIndex)) continue;

            _squareBoardController.PutSquareOnBoard(_gameBoardController, startIndex - 1);
        }
    }

    private void OnSquareAssigned(IBoardController board, ICellController cell, ISquareController square)
    {
        if (_squareOnBoards == null) return;

        _squareOnBoards[square] = board;

        if (board != null && board == _gameBoardController)
        {
            if (_occupiedGoalCells == null || cell == null) return;

            if (_occupiedGoalCells.ContainsKey(cell))
            {
                _occupiedGoalCells[cell] = true; 
            }

            if (IsVictory())
            {
                if (_coroutineHandler != null)
                    _coroutineHandler.HandleCoroutine(WinSequence());
                else
                {
                    ClearGameData();
                    GameStateHandler.ChangeGameState(GameState.Win);
                }
            }
            else if (!HasPlayableSquaresLeft())
            {
                ClearGameData();
                GameStateHandler.ChangeGameState(GameState.Lose);
            }
        }
    }

    private bool IsVictory()
    {
        foreach (var kvpair in _occupiedGoalCells)
        {
            if (!kvpair.Value) return false;
        }        

        return true;
    }

    private bool HasPlayableSquaresLeft()
    {
        var squares = _squareBoardController.Squares;
        if (squares != null)
        {
            if (squares.Length > _squareOnBoards.Count) return true;
        }

        foreach (var kvpair in _squareOnBoards)
        {
            if (kvpair.Value != _gameBoardController) return true;
        }

        return false;
    }

    public void RegisterGameDataSaver()
    {
        SaveHandlerController.RegisterGameDataSaver(this, out _gameDataSO);
    }

    public void SaveGameData()
    {
        if (_squareOnBoards == null || _gameDataSO == null || _squareBoardController == null) return;

        _gameDataSO.PlayerBoardIndeces.Clear();
        _gameDataSO.GameBoardIndeces.Clear();

        foreach (var kvpair in _squareOnBoards)
        {
            var square = kvpair.Key;
            var boardController = kvpair.Value;
            if (boardController == null) continue;
            var boardIndeces = boardController == _gameBoardController ? _gameDataSO.GameBoardIndeces : _gameDataSO.PlayerBoardIndeces;
            var cell = _squareBoardController.GetCell(square);
            boardIndeces.Add(boardController.GetCellIndex(cell));
        }
    }

    public void ClearGameData()
    {
        if (_gameDataSO == null) return;
        _gameDataSO.PlayerBoardIndeces.Clear();
        _squareOnBoards.Clear();
    }

    private IEnumerator WinSequence()
    {
        foreach (var kvpair in _squareOnBoards)
        {
            var square = kvpair.Key;
            square.LockInPlace();
            var boardController = kvpair.Value;
            if (boardController == null || boardController == _playerBoardController) continue;

            square.Disappear();
        }

        yield return new WaitForSeconds(1.0f);

        ClearGameData();

        GameStateHandler.ChangeGameState(GameState.Win);
    }

}
