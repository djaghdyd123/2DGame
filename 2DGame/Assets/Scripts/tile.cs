﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class tile : MonoBehaviour
{
    public Tilemap _tilemap;
    public TileBase _tilebase;

    void Start()
    {
        _tilemap.SetTile(new Vector3Int(0, 0, 0), _tilebase);
    }

    void Update()
    {
        
    }
}
