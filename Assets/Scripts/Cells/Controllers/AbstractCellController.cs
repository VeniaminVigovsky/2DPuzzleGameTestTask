using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AbstractCellController : ICellController
{
    public event Action<ICellController, ISquareController> SquareDropped;

    public event Action<ICellController, ISquareController> SquareAssigned;
    public RectTransform RectTransform => _rectTransform;

    protected RectTransform _rectTransform;

    public ISquareController Square { get; protected set; }

    public AbstractCellController(RectTransform rectTransform)
    {
        _rectTransform = rectTransform;
    }

    public virtual void AssignSquare(ISquareController square)
    {
        if (square == null) return;
        SquareAssigned?.Invoke(this, square);
    }

    public virtual void OnDrop(PointerEventData eventData)
    {
        var squareView = eventData.pointerDrag.GetComponent<ISquareView>();

        if (squareView != null)
        {
            var squareController = squareView.SquareController;
            SquareDropped?.Invoke(this, squareController);
        }
    }
}
