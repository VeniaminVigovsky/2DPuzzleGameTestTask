using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameBoardCellView : AbstractBoardCellView
{
    public override void Init()
    {
        if (_isInit) return;

        var rectTransform = GetComponent<RectTransform>();

        _cellController = new GameBoardCellController(rectTransform);

        _isInit = true;
    }
}
