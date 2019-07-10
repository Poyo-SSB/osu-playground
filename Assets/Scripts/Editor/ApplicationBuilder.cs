using System;
using System.IO;
using UnityEditor;

public static class ApplicationBuilder
{
    public static void Build()
    {
        var args = Environment.GetCommandLineArgs();

        string version = null;
        string buildFolder = null;

        for (int i = 0; i < args.Length; i++)
        {
            if (args[i].ToUpperInvariant() == "-VERSION" && (i + 1) < args.Length)
            {
                version = args[i + 1];
            }
            if (args[i].ToUpperInvariant() == "-BUILDFOLDER" && (i + 1) < args.Length)
            {
                buildFolder = args[i + 1];
            }
        }

        if (version == null || buildFolder == null)
        {
            EditorApplication.Exit(7139);
            return;
        }

        var scenes = new[] { "Assets/Scenes/Screen.unity" };

        BuildPipeline.BuildPlayer(new BuildPlayerOptions
        {
            locationPathName = Path.Combine(buildFolder, $"{version}/osuPlayground-{version}-win32/osuPlayground.exe"),
            scenes = scenes,
            targetGroup = BuildTargetGroup.Standalone,
            target = BuildTarget.StandaloneWindows
        });
        BuildPipeline.BuildPlayer(new BuildPlayerOptions
        {
            locationPathName = Path.Combine(buildFolder, $"{version}/osuPlayground-{version}-win64/osuPlayground.exe"),
            scenes = scenes,
            targetGroup = BuildTargetGroup.Standalone,
            target = BuildTarget.StandaloneWindows64
        });
        BuildPipeline.BuildPlayer(new BuildPlayerOptions
        {
            locationPathName = Path.Combine(buildFolder, $"{version}/osuPlayground-{version}-mac/osuPlayground.app"),
            scenes = scenes,
            targetGroup = BuildTargetGroup.Standalone,
            target = BuildTarget.StandaloneOSX
        });
        BuildPipeline.BuildPlayer(new BuildPlayerOptions
        {
            locationPathName = Path.Combine(buildFolder, $"{version}/osuPlayground-{version}-linux/osuPlayground.x86_64"),
            scenes = scenes,
            targetGroup = BuildTargetGroup.Standalone,
            target = BuildTarget.StandaloneLinux64
        });
    }
}
