using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoardView : AbstractBoardView
{
    public override IBoardController GetBoardController(ICellController[] cellControllers)
    {
        return new PlayerBoardController(cellControllers);
    }
}
