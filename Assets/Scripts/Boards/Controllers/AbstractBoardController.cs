using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractBoardController : IBoardController
{
    public event Action<IBoardController, ICellController, ISquareController> SquareAssigned;

    public ICellController[] Cells { get; private set; }

    public AbstractBoardController(ICellController[] cells)
    {
        if (cells == null) return;

        Cells = cells;

        foreach (var cell in cells)
        {
            cell.SquareDropped += OnSquareDropped;
            cell.SquareAssigned += OnSquareAssigned;
        }
    }


    public virtual void OnSquareDropped(ICellController cellController, ISquareController squareController)
    {
        if (cellController == null || squareController == null) return;

        squareController.TryAssignToCell(cellController);       
    }

    public virtual void OnSquareAssigned(ICellController cell, ISquareController square)
    {
        SquareAssigned?.Invoke(this, cell, square);
    }

    public virtual int GetCellIndex(ICellController cellController)
    {
        if (Cells == null) return -1;

        var N = Cells.Length;

        for (int i = 0; i < N; i++)
        {
            if (cellController == Cells[i]) return i;
        }

        return -1;
    }
}
