using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MultiplayerBuildnRun
{

    [MenuItem("Tools/Run Multiplayer/2Players")]
    static void PerformOsxBuild2()
    {
        PerformOsxBuild(2);
    }
    [MenuItem("Tools/Run Multiplayer/3Players")]
    static void PerformOsxBuild3()
    {
        PerformOsxBuild(3);
    }
    [MenuItem("Tools/Run Multiplayer/4Players")]
    static void PerformOsxBuild4()
    {
        PerformOsxBuild(4);
    }
    static void PerformOsxBuild(int playerCount)
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneOSX);

        for(int i = 1; i <=playerCount;i++)
        {
            BuildPipeline.BuildPlayer(GetScenePaths(), "builds/" + GetProjectName() + i.ToString(),BuildTarget.StandaloneOSX, BuildOptions.AutoRunPlayer);
        }
    }

    static string GetProjectName()
    {
        //현재 프로젝트 경로중 프로젝트 이름만 따오기
        string[] s = Application.dataPath.Split('/');
        return s[s.Length - 2];

    }

    static string[] GetScenePaths()
    {
        string[] scenes = new string[EditorBuildSettings.scenes.Length];
        for(int i = 0; i < scenes.Length; i++)
        {
            scenes[i] = EditorBuildSettings.scenes[i].path;
        }
        return scenes;
    }
}
