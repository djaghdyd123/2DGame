using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Protobuf.Protocol;
public class ObjectManager
{
    public MyPlayerController MyPlayer{get;set;}
    // 원래는 collision 배열 처럼 또다른 배열로 object collision 관리가 좋음.
    Dictionary<int,GameObject> _objects = new Dictionary<int, GameObject>();
    public void Add(PlayerInfo info, bool myPlayer = false)
    {
        if(myPlayer)
        {
            GameObject player = Managers.Resources.Instantiate("Creature/MyPlayer");
            player.name = info.Name;
            _objects.Add(info.PlayerId, player);

            MyPlayer = player.GetComponent<MyPlayerController>();
            MyPlayer.Id = info.PlayerId;
            MyPlayer.CellPos = new Vector3Int(info.PosX, info.PosY ,0);
        }
        else
        {
            GameObject player = Managers.Resources.Instantiate("Creature/Player");
            player.name = info.Name;
            _objects.Add(info.PlayerId, player);

            PlayerController pc = player.GetComponent<PlayerController>();
            pc.Id = info.PlayerId;
            pc.CellPos = new Vector3Int(info.PosX, info.PosY, 0);
        }
    }
    public void Add(int id,GameObject go)
    {
        _objects.Add(id,go);
    }

    public void Remove(int id)
    {
        _objects.Remove(id);
    }

    public void RemoveMyPlayer()
    {
        if(MyPlayer== null)
        return;
        Remove(MyPlayer.Id);
        MyPlayer = null;

    }
    public GameObject Find(Vector3Int cellPos)
    {
        foreach (GameObject obj in _objects.Values)
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
        foreach (GameObject obj in _objects.Values)
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
