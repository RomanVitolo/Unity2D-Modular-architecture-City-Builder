﻿using UnityEditor;
using UnityEngine;

// if you want to delete this file delete all Gentleland "Utils" folder 
// you can then delete GentlelandSettings folder too
namespace Gentleland.SteampunkUI.Utils.Editor
{
    [InitializeOnLoad]
    public static class OpenWindowsOnLoad
    {
        static OpenWindowsOnLoad()
        {
            PackageSettings settings = AssetDatabase.LoadAssetAtPath<PackageSettings>(PackageSettings.PackageSettingsPath);
            if (settings == null)
            {
                if (!AssetDatabase.IsValidFolder(PackageSettings.PackageSettingsFolderPath))
                {
                    AssetDatabase.CreateFolder("Assets",PackageSettings.PackageSettingsFolder);
                }
                settings = ScriptableObject.CreateInstance<PackageSettings>();
                AssetDatabase.CreateAsset(settings, PackageSettings.PackageSettingsPath);
            }
            if (settings.isFirstTimeUsingTheAsset)
            {
                EditorApplication.delayCall += WelcomeWindow.OpenWindow;
            }
        }
    }
}
