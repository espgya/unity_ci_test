#if UNITY_EDITOR

using System;
using UnityEditor;
using System.IO;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace BuildAndDeployGame
{
    public static class BuildScript
    {
        private static void BuildWebGl()
        {
            const string folderName = "Build-WebGl";
            DeleteFolder(folderName);
            CreateFolder(folderName);
            
            var buildPlayerOptions = new BuildPlayerOptions
            {
                scenes = new[] { "Assets/Scenes/SampleScene.unity" }, //GetScenes(),
                locationPathName = folderName,
                target = BuildTarget.WebGL,
                options = BuildOptions.None
            };
            
            var report = BuildPipeline.BuildPlayer(buildPlayerOptions);
            var summary = report.summary;
            
            switch (summary.result)
            {
                case BuildResult.Succeeded:
                    Debug.Log($"Build succeeded: {summary.totalSize.ToString()} bytes");
                    break;
                case BuildResult.Failed:
                    Debug.Log($"Build failed with {report.summary.totalErrors} errors and {report.summary.totalWarnings} warnings.");
                    break;
                
                case BuildResult.Unknown:
                case BuildResult.Cancelled:
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static void CreateFolder(string name)
        {
            if (Directory.Exists(name)) return;
            
            Directory.CreateDirectory(name);
        }

        private static void DeleteFolder(string name)
        {
            if (Directory.Exists(name))
                Directory.Delete(name, true);
        }

        // private static string[] GetScenes()
        // {
        //     var scenes = new string[ EditorBuildSettings.scenes.Length ];
        //     for(var i = 0; i <= scenes.Length; i++)
        //     {
        //         scenes[i] = EditorBuildSettings.scenes[i].path;
        //     }
        //     
        //     return scenes;
        // }
    }
}
#endif