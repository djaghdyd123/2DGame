﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{

    public Define.Scene SceneType { get; protected set; } = Define.Scene.Unknown;

    void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        Object go = GameObject.FindObjectOfType(typeof(EventSystem));
        if (go == null)
            Managers.Resources.Instantiate("UI/EventSystem").name = "@EventSystem";
    }

    public abstract void Clear();

}