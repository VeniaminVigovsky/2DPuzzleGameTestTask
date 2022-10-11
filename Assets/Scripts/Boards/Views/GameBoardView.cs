using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoardView : AbstractBoardView
{
    public override IBoardController GetBoardController(ICellController[] cellControllers)
    {
        return new GameBoardController(cellControllers);
    }
}
