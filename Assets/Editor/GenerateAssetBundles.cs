using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class GenerateAssetBundles
{
    static string ATTACHMENT_ROOT_PATH = "Assets/Prefab/customization/attachments/roof/";
    static string BUNDLES_PATH = "AssetBundles/";


    [MenuItem("Assets/Generate Asset bundles - Windows")]
    static public void GenerateWindows()
    {
        GenerateBundles(BuildTarget.StandaloneWindows64, "win");
    }

    [MenuItem("Assets/Generate Asset bundles - OSX")]
    static public void GenerateOSX()
    {
        GenerateBundles(BuildTarget.StandaloneOSX, "mac");
    }

    static void GenerateBundles(BuildTarget buildTarget, string prefix)
    {
        List<string> bundleNames = new List<string>();

        string[] directories = Directory.GetDirectories(ATTACHMENT_ROOT_PATH);

        foreach (string dir in directories)
        {
            string[] _files = Directory.GetFiles(dir);

            foreach (string filePath in _files)
            {
                if(Path.GetExtension(filePath) != ".meta")
                    bundleNames.Add(GenerateBundleWin(buildTarget, prefix, filePath));
            }
        }

        string[] files = Directory.GetFiles(ATTACHMENT_ROOT_PATH);

        foreach (string filePath in files)
        {
            if (Path.GetExtension(filePath) != ".meta")
                bundleNames.Add(GenerateBundleWin(buildTarget, prefix, filePath));
        }

        AssetBundleManager.BundleManifest manifest = new AssetBundleManager.BundleManifest();

        manifest.bundles = new List<AssetBundleManager.Bundle>();

        foreach(string name in bundleNames)
        {
            AssetBundleManager.Bundle bundle = new AssetBundleManager.Bundle();

            bundle.name = name;
            bundle.version = int.Parse(Application.version);

            manifest.bundles.Add(bundle);
        }

        File.WriteAllText(BUNDLES_PATH + "manifest.json", JsonUtility.ToJson(manifest));
    }

    static string GenerateBundleWin(BuildTarget target, string suffix, string filePath)
    {
        string[] assets = new string[2];

        assets[0] = filePath;
        //assets[1] = file + ".png";

        AssetBundleBuild[] buildMap = new AssetBundleBuild[1];

        string bundleName = Path.GetFileName(filePath) + "_" + suffix;

        buildMap[0].assetBundleName = bundleName;
        buildMap[0].assetNames = assets;

        BuildPipeline.BuildAssetBundles(BUNDLES_PATH, buildMap, BuildAssetBundleOptions.ChunkBasedCompression, target);

        return bundleName;
    }
}
