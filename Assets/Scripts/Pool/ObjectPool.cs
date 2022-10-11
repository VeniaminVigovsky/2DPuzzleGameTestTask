using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    public List<T> Pool => _pool;

    private T _original;

    private List<T> _pool;
    private Transform _parent;

    public ObjectPool(T original, int size, Transform parent = null)
    {
        _pool = new List<T>(size);

        _original = original;
        _parent = parent;

        GrowPool(size);
    }

    private void GrowPool(int size)
    {
        for (int i = 0; i < size; i++)
        {
            AddToPool();
        }
    }

    private T AddToPool()
    {
        var newPoolItem = Object.Instantiate(_original, _parent);
        newPoolItem.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        newPoolItem.transform.localRotation = Quaternion.identity;

        newPoolItem.gameObject.SetActive(false);

        _pool.Add(newPoolItem);

        return newPoolItem;
    }

    public T GetFromPool()
    {
        foreach (var item in _pool)
        {
            if (!item.gameObject.activeInHierarchy)
            {
                item.gameObject.SetActive(true);
                return item;
            }
        }

        var itemToReturn = AddToPool();
        itemToReturn.gameObject.SetActive(true);
        return itemToReturn;

    }
}
