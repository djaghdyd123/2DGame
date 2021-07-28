using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager
{
    // 원래는 collision 배열 처럼 또다른 배열로 object collision 관리가 좋음.
    List<GameObject> _objects = new List<GameObject>();

    public void Add(GameObject go)
    {
        _objects.Add(go);
    }

    public void Remove(GameObject go)
    {
        _objects.Remove(go);
    }

    public GameObject Find(Vector3Int cellPos)
    {
        foreach (GameObject obj in _objects)
        {
            CreatureController cc = obj.GetComponent<CreatureController>();
            if (cc == null)
                continue;
            if (cc.CellPos == cellPos)
                return obj;
        }
        return null;
    }

    public GameObject Find(Func<GameObject,bool> condition)
    {
        foreach (GameObject obj in _objects)
        {
            if (condition.Invoke(obj))
                return obj;
        }
        return null;
    }

    public void Clear()
    {
        _objects.Clear();
    }
}
