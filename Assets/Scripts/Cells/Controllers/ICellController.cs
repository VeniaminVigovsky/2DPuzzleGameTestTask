using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface ICellController
{
    event Action<ICellController, ISquareController> SquareDropped;
    event Action<ICellController, ISquareController> SquareAssigned;
    RectTransform RectTransform { get; }
    void OnDrop(PointerEventData eventData);
    void AssignSquare(ISquareController square);
}
