using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : CreatureController
{
    protected override void Init()
    {
        base.Init();
        State = Define.CreatureState.Idle;
        Dir = Define.MoveDir.None;
    }


    protected override void UpdateController()
    {
        //GetDirInput();
        base.UpdateController();
    }
}
