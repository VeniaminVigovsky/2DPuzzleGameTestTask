using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SquareController : ISquareController
{    
    public event Action<ISquareController> SquareDragEnded;
    public event Action<ISquareController, ICellController> CellChanged;

    public bool IsLocked => _isLocked;

    private RectTransform _selfT;
    private Image _squareImage;

    private CanvasGroup _canvasGroup;
    private Transform _gameCanvasTransform;

    private ICoroutineHandler _coroutineHandler;
    
    private bool _isLocked;


    public SquareController(RectTransform rectTransform, Image squareImage, ICoroutineHandler coroutineHandler)
    {
        _selfT = rectTransform;
        _squareImage = squareImage;
        _coroutineHandler = coroutineHandler;
        _canvasGroup = rectTransform.GetComponent<CanvasGroup>();
        _gameCanvasTransform = FindFirstParent(_selfT);
        _isLocked = false;
    }

    public void LockInPlace()
    {
        _isLocked = true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_isLocked) return;

        _canvasGroup.blocksRaycasts = false;
        SetParent(_gameCanvasTransform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_isLocked) return;

        SetPosition(eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = true;        

        SquareDragEnded?.Invoke(this);
    }

    private void SetPosition(Vector3 position)
    {
        _selfT.position = position;
    }

    private void SetParent(Transform transform)
    {
        if (transform == null) return;

        _selfT.SetParent(transform);
    }

    private Transform FindFirstParent(Transform t)
    {
        var parent = t.parent;
        if (parent == null)
            return t;
        else return FindFirstParent(parent);
    }

    public void TryAssignToCell(ICellController cell)
    {
        if (cell == null ||            
            cell.RectTransform == null || _isLocked) return;        

        CellChanged?.Invoke(this, cell);
    }

    public void MoveToCell(ICellController cell)
    {
        if (cell == null) return;
        var transform = cell.RectTransform;
        var pos = transform != null ? transform.position : _selfT.position;

        SetParent(transform);
        SetPosition(pos);
    }

    public void Disappear()
    {
        _coroutineHandler?.HandleCoroutine(SetAlphaOverTime(0.0f));
    }

    private IEnumerator SetAlphaOverTime(float targetAlpha)
    {
        if (_squareImage == null) yield break;
        
        Color c = _squareImage.color;
        var alpha = c.a;

        while (alpha > targetAlpha && alpha >= 0.0f)
        {
            var squareColor = new Color(c.r, c.g, c.b, alpha);
            _squareImage.color = squareColor;
            alpha -= 0.1f;            
            yield return new WaitForSeconds(0.08f);
        }

        _squareImage.color = new Color(c.r, c.g, c.b, 0.0f);

    }
}
