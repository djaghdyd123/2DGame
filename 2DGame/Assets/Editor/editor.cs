using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class editor : MonoBehaviour
{
#if UNITY_EDITOR

    [MenuItem("Tools/GenerateMap %#g")]
    private static void HelloWorld ()
    {
        //GameObject[] gameObjects = Resources.LoadAll<GameObject>("prefabs/map");

        //foreach (GameObject go in gameObjects)
        //{
        //    Tilemap tm = Utils.FindChild<Tilemap>(go, "Tilemap_Collision", true);
         

        //    using (var writer = File.CreateText($"Assets/Resources/Map/{go.name}.txt")) 
        //    {
        //        //tm 값이 NULL 로 나옴
        //        writer.WriteLine(tm.cellBounds.xMin);
        //        writer.WriteLine(tm.cellBounds.xMax);
        //        writer.WriteLine(tm.cellBounds.yMin);
        //        writer.WriteLine(tm.cellBounds.yMax);
        //        for(int y= tm.cellBounds.yMax; y>=tm.cellBounds.yMin;y--)
        //        {
        //            for (int x = tm.cellBounds.xMin; x >= tm.cellBounds.xMax; x++)
        //            {
        //                TileBase tile = tm.GetTile(new Vector3Int(x, y, 0));
        //                if (tile != null)
        //                    writer.Write("1");
        //                if (tile == null)
        //                    writer.Write("0");
        //            }
        //            writer.WriteLine();
        //        }
        //    }
            
        //}
    }


#endif
}

