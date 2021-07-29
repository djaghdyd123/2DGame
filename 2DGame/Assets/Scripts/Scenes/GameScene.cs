using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;
        Managers.Map.LoadMap(1);

        Screen.SetResolution(640, 480, false);

        //GameObject player = Managers.Resources.Instantiate("Creature/Player");
        //player.name = "Player";

        //Managers.Object.Add(player);

        //for (int i = 0; i < 3; i++)
        //{
        //    GameObject monster = Managers.Resources.Instantiate("Creature/Monster");
        //    monster.name = $"Monster{i}";

        //    Vector3Int pos = new Vector3Int()
        //    {
        //        x = Random.Range(-10, 10),
        //        y = Random.Range(-5, 5)
        //    };
        //    MonsterController mc = monster.GetComponent<MonsterController>();
        //    mc.CellPos = pos;
        //    Managers.Object.Add(monster);
        //}
    }

    public override void Clear()
    {

    }


}