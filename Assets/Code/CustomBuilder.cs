using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global"), 
     SuppressMessage("ReSharper", "InconsistentNaming"), 
     SuppressMessage("ReSharper", "CheckNamespace")]
    public class CustomBuilder
    {
        static void AndroidDevelopment() 
        {
            //PlayerSettings.SetScriptingBackend (BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
            //EditorUserBuildSettings.SwitchActiveBuildTarget (BuildTargetGroup.Android, BuildTarget.Android);
            //EditorUserBuildSettings.development = true;
            //BuildReport report = BuildPipeline.BuildPlayer (GetScenes (), "/Users/ilakocura/BuildsCi/testBuild.apk", BuildTarget.Android, BuildOptions.None);
            //int code = (report.summary.result == BuildResult.Succeeded) ? 0 : 1;       

            var a = Random.Range(2, 5);
            var b = Random.Range(2, 5);

            int exitCode = 0;
            
            if (a == b)
            {
                exitCode = 1;
            }
            
            EditorApplication.Exit(exitCode);
            
            Console.WriteLine(1);
            //EditorApplication.Exit(code);   
        }
        
        static string[] GetScenes()
        {
            var projectScenes = EditorBuildSettings.scenes;
            List<string> scenesToBuild = new List<string>();
            for (int i = 0; i < projectScenes.Length; i++)
            {
                if (projectScenes[i].enabled) {
                    scenesToBuild.Add(projectScenes[i].path);
                }
            }
            return scenesToBuild.ToArray();
        }
    }
}