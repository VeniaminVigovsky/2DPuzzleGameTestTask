using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareBoardController : ISquareBoardController
{
    public ISquareController[] Squares 
    {
        get
        {
            if (_activeControllers == null)
                _activeControllers = new List<ISquareController>();

            return _activeControllers.ToArray();
        } 
    }

    private Dictionary<ICellController, ISquareController> _cells;
    private Dictionary<ISquareController, ICellController> _squares;


    private ObjectPool<SquareView> _squareViewPool;
    private List<ISquareController> _activeControllers;
    

    public SquareBoardController(SquareView squareViewPrefab, Transform squareBoardTransform)
    {
        _squareViewPool = new ObjectPool<SquareView>(squareViewPrefab, 3, squareBoardTransform);
        _activeControllers = new List<ISquareController>();

        _cells = new Dictionary<ICellController, ISquareController>();
        _squares = new Dictionary<ISquareController, ICellController>();
    }

    public void PutSquareOnBoard(IBoardController boardController, int cellIndex)
    {
        if (_squareViewPool == null) return;

        var squareView = _squareViewPool.GetFromPool();
        var squareController = squareView.SquareController;

        var cellControllers = boardController.Cells;
        if (cellControllers == null || cellControllers.Length < cellIndex) return;

        var cellController = cellControllers[cellIndex];

        _squares.Add(squareController, null);
        squareController.CellChanged += OnCellChanged;
        squareController.SquareDragEnded += OnSquareDragEnded;

        OnCellChanged(squareController, cellController);
        OnSquareDragEnded(squareController);

        squareView.gameObject.SetActive(true);

        _activeControllers.Add(squareController);
    }

    public ICellController GetCell(ISquareController square)
    {
        if (_squares == null || !_squares.ContainsKey(square)) return null;

        return _squares[square];
    }

    public void OnCellChanged(ISquareController squareController, ICellController cellController)
    {
        if (_cells.ContainsKey(cellController))
        {
            if (_cells[cellController] != null)
                return;            
        }

        _cells[cellController] = squareController;
        cellController?.AssignSquare(squareController);

        if (_squares.TryGetValue(squareController, out var oldCell) && oldCell != null)
        { 
            _cells[oldCell] = null;
        }

        _squares[squareController] = cellController;
    }

    public void OnSquareDragEnded(ISquareController squareController)
    {
        if (_squares.TryGetValue(squareController, out var currentCell))
        {
            squareController.MoveToCell(currentCell);
        }
    }
}
