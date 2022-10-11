using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractBoardView : MonoBehaviour, IBoardView
{
    public IBoardController BoardController
    {
        get
        {
            if (!_isInit) Init();

            return _boardController;
        }
    }

    [SerializeField] private AbstractBoardCellView[] _cellViews;

    protected IBoardController _boardController;

    protected bool _isInit;

    public virtual void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        if (_isInit) return;

        var controllers = GetControllers(_cellViews);
        _boardController = GetBoardController(controllers);

        _isInit = true;
    }
    public virtual ICellController[] GetControllers(ICellView[] cellViews)
    {
        if (cellViews == null) return null;

        var controllers = new List<ICellController>();

        foreach (var cellView in cellViews)
        {
            controllers.Add(cellView.CellController);
        }

        return controllers.ToArray();
    }

    public abstract IBoardController GetBoardController(ICellController[] cellControllers);

}
