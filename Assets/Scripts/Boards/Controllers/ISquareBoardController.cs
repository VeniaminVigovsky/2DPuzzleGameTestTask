using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISquareBoardController
{    
    public ISquareController[] Squares { get; }
    void OnCellChanged(ISquareController squareController, ICellController cellController);
    void OnSquareDragEnded(ISquareController squareController);
    void PutSquareOnBoard(IBoardController boardController, int cellIndex);
    ICellController GetCell(ISquareController square);
}
