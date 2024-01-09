using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEditor;
using UnityEditor.Build.Reporting;

namespace Code
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global"), 
     SuppressMessage("ReSharper", "InconsistentNaming"), 
     SuppressMessage("ReSharper", "CheckNamespace")]
    public class CustomBuilder
    {
        static void AndroidDevelopment() {
            PlayerSettings.SetScriptingBackend (BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
            PlayerSettings.SetScriptingDefineSymbolsForGroup (BuildTargetGroup.Android, "DEV");
            EditorUserBuildSettings.SwitchActiveBuildTarget (BuildTargetGroup.Android, BuildTarget.Android);
            EditorUserBuildSettings.development = true;
            EditorUserBuildSettings.androidETC2Fallback = AndroidETC2Fallback.Quality32Bit;
            //BuildReport report = BuildPipeline.BuildPlayer (GetScenes (), "/Users/BuildCi/testBuild.apk", BuildTarget.Android, BuildOptions.None);
            //int code = (report.summary.result == BuildResult.Succeeded) ? 0 : 1;       
            EditorApplication.Exit(0);   
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