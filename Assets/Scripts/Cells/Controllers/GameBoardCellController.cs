using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoardCellController : AbstractCellController
{
    public GameBoardCellController(RectTransform rectTransform) : base(rectTransform)
    {
    }

    public override void AssignSquare(ISquareController square)
    {        
        base.AssignSquare(square);
        square?.LockInPlace();
    }
}
