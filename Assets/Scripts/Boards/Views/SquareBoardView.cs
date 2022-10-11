using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareBoardView : MonoBehaviour, ISquareBoardView
{
    public ISquareBoardController SquareBoardController
    {
        get
        {
            if (!_isInit) Init();
            return _squareBoardController;
        }
    }

    [SerializeField] private SquareView _squareViewPrefab;

    private ISquareBoardController _squareBoardController;

    private bool _isInit;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (_isInit) return;

        _squareBoardController = new SquareBoardController(_squareViewPrefab, transform);

        _isInit = true;
    }
}
