using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.Build.Reporting;
using System;

public static class Builder
{
    public const string BuildPath = "Build";


    [MenuItem("Kon Build/Delete Build Folder")]
    public static void DeleteBuildFolder()
    {
        if (Directory.Exists(BuildPath))
        {
            Directory.Delete(BuildPath, true);
        }
    }


    private static string[] GetListOfScenes()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        string[] scenes = new string[sceneCount];

        for (int i = 0; i < sceneCount; i++)
        {
            scenes[i] = SceneUtility.GetScenePathByBuildIndex(i);
        }

        return scenes;
    }


    [MenuItem("Kon Build/Increment Build Number")]
    public static void IncrementBuildNumber()
    {
        string settingAssetPath = "./ProjectSettings/ProjectSettings.asset";

        // Read in file contents
        string fileContents = File.ReadAllText(settingAssetPath);

        // Find version string
        int startIndex = fileContents.IndexOf("bundleVersion:", StringComparison.Ordinal);
        if (startIndex == -1)
        {
            return;
        }
        int endIndex = fileContents.IndexOf("\n", startIndex);
        string versionString = fileContents.Substring(startIndex, endIndex - startIndex);

        // Split this string into an array of 3 numbers
        string[] numbers = versionString.Split(".");

        // Convert last string to a number, this will be the build number
        string buildString = numbers[^1];
        int buildNumber = Int32.Parse(buildString);

        // Incremenet the build number
        buildNumber++;

        // Replace the last string in the array with our new buildNumber
        numbers[^1] = $"{buildNumber}";

        // Convert array of multiple strings into a single string
        string newVersionString = string.Join(".", numbers);

        // Replace the version string in the file contents with the new version string and save contents back to disk
        fileContents = fileContents.Replace(versionString, newVersionString);
        File.WriteAllText(settingAssetPath, fileContents);
    }



    [MenuItem("Kon Build/Build iOS")]
    public static void BuildIOS()
    {
        IncrementBuildNumber();

        string buildPath = BuildPath + "/iOS_Device";
        string buildName = "iOS Build";

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.locationPathName = buildPath;
        buildPlayerOptions.scenes = GetListOfScenes();
        buildPlayerOptions.target = BuildTarget.iOS;

        // Causes crash on the CI
        //buildPlayerOptions.options = BuildOptions.AcceptExternalModificationsToPlayer;

        // Refresh assets
        if (Application.isBatchMode)
        {
            AssetDatabase.Refresh();
        }

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log($"{buildName} succeeded: {summary.totalSize} bytes");
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.LogError($"{buildName} failed");
        }
    }


    [MenuItem("Kon Build/Build Web")]
    public static void BuildWeb()
    {
        IncrementBuildNumber();

        string buildPath = BuildPath + "/WebGL/game";
        string buildName = "kon";

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.locationPathName = buildPath;
        buildPlayerOptions.scenes = GetListOfScenes();
        buildPlayerOptions.target = BuildTarget.WebGL;

        // Causes crash on the CI
        //buildPlayerOptions.options = BuildOptions.AcceptExternalModificationsToPlayer;

        // Refresh assets
        if (Application.isBatchMode)
        {
            AssetDatabase.Refresh();
        }

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log($"{buildName} succeeded: {summary.totalSize} bytes");
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.LogError($"{buildName} failed");
        }
    }
}
