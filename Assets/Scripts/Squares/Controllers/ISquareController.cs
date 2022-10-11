using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public interface ISquareController
{
    event Action<ISquareController> SquareDragEnded;

    event Action<ISquareController, ICellController> CellChanged;

    bool IsLocked { get; }

    void OnBeginDrag(PointerEventData eventData);

    void OnDrag(PointerEventData eventData);

    void OnEndDrag(PointerEventData eventData);

    void LockInPlace(); 

    void TryAssignToCell(ICellController cell);
    void MoveToCell(ICellController cell);
    void Disappear();
}
