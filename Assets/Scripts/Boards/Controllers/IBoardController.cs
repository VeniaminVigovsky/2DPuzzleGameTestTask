using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IBoardController
{
    event Action<IBoardController, ICellController, ISquareController> SquareAssigned;
    ICellController[] Cells { get; }
    void OnSquareDropped(ICellController cellController, ISquareController squareController);
    int GetCellIndex(ICellController cellController);
}


