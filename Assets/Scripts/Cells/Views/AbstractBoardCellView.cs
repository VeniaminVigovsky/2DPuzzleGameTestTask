using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class AbstractBoardCellView : MonoBehaviour, ICellView, IDropHandler
{
    public ICellController CellController 
    {
        get
        {
            if (!_isInit) Init();
            return _cellController;
        }
    }

    protected ICellController _cellController;

    protected bool _isInit;

    public virtual void Awake()
    {
        Init();
    }

    public abstract void Init();

    public virtual void OnDrop(PointerEventData eventData)
    {
        if (!_isInit) Init();

        CellController?.OnDrop(eventData);
    }

}
