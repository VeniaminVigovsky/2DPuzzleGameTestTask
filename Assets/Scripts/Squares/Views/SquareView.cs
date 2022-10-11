using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SquareView : MonoBehaviour, ISquareView, IDragHandler, IBeginDragHandler, IEndDragHandler, ICoroutineHandler
{
    public ISquareController SquareController
    {
        get
        {
            if (!_isInit) Init();
            return _squareController;
        }
    }

    [SerializeField] private Image _squareImage;

    private ISquareController _squareController;

    private bool _isInit;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (_isInit) return;

        var rectTransform = GetComponent<RectTransform>();        

        _squareController = new SquareController(rectTransform, _squareImage, this);

        _isInit = true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!_isInit) Init();

        SquareController?.OnBeginDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_isInit) Init();

        SquareController?.OnDrag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!_isInit) Init();

        SquareController?.OnEndDrag(eventData);
    }

    public void HandleCoroutine(IEnumerator coroutine)
    {
        StopAllCoroutines();
        StartCoroutine(coroutine);
    }
}
