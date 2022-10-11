using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoardCellView : AbstractBoardCellView
{
    public override void Init()
    {
        if (_isInit) return;

        var rectTransform = GetComponent<RectTransform>();

        _cellController = new PlayerBoardCellContoller(rectTransform);

        _isInit = true;
    }
}
