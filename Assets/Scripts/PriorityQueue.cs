using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityQueue<T>
{
    Dictionary<T, int> _allElements = new Dictionary<T, int>();
    public int Count { get { return _allElements.Count; } }
    public void Enqueue(T elem, int cost)
    {
        if (!_allElements.ContainsKey(elem))
            _allElements.Add(elem, cost);
        else
            _allElements[elem] = cost;
    }

    public T Dequeue()
    {
        float lowestValue = Mathf.Infinity;
        T elem = default;

        foreach(var item in _allElements)
        {
            if(item.Value < lowestValue)
            {
                lowestValue = item.Value;
                elem = item.Key;
            }
        }

        _allElements.Remove(elem);
        return elem;
    }
}
