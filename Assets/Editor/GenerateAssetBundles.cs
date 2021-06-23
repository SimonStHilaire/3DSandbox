using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class GenerateAssetBundles
{
    static string ATTACHMENT_ROOT_PATH = "Assets/Prefab/customization/attachments/";
    static string VEHICULES_ROOT_PATH = "Assets/Prefab/customization/cars/";
    static string ATTACHMENTS_MANIFEST_FILENAME = "manifest.json";
    static string CARS_MANIFEST_FILENAME = "carsmanifest.json";
    static string BUNDLES_PATH = "AssetBundles/";


    [MenuItem("Assets/Generate Attachments Asset bundles - Windows")]
    static public void GenerateAttachmentsWindows()
    {
        GenerateBundles(BuildTarget.StandaloneWindows64, ATTACHMENT_ROOT_PATH, ATTACHMENTS_MANIFEST_FILENAME, "win");
    }

    [MenuItem("Assets/Generate Cars Asset bundles - Windows")]
    static public void GenerateCarsWindows()
    {
        GenerateBundles(BuildTarget.StandaloneWindows64, VEHICULES_ROOT_PATH, CARS_MANIFEST_FILENAME, "win");
    }

    [MenuItem("Assets/Generate Attachments Asset bundles - OSX")]
    static public void GenerateAttachmentsOSX()
    {
        GenerateBundles(BuildTarget.StandaloneOSX, ATTACHMENT_ROOT_PATH, ATTACHMENTS_MANIFEST_FILENAME, "mac");
    }

    [MenuItem("Assets/Generate Cars Asset bundles - OSX")]
    static public void GenerateCarsOSX()
    {
        GenerateBundles(BuildTarget.StandaloneOSX, VEHICULES_ROOT_PATH, CARS_MANIFEST_FILENAME, "mac");
    }

    static void GenerateBundles(BuildTarget buildTarget, string sourceFolder, string manifestFilename, string prefix)
    {
        List<string> bundleNames = new List<string>();

        string[] directories = Directory.GetDirectories(sourceFolder);

        foreach (string dir in directories)
        {
            string[] _files = Directory.GetFiles(dir);

            foreach (string filePath in _files)
            {
                if(Path.GetExtension(filePath) == ".prefab")
                    bundleNames.Add(GenerateBundleWin(buildTarget, prefix, filePath));
            }
        }

        string[] files = Directory.GetFiles(sourceFolder);

        foreach (string filePath in files)
        {
            if (Path.GetExtension(filePath) == ".prefab")
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

        File.WriteAllText(BUNDLES_PATH + manifestFilename, JsonUtility.ToJson(manifest));
    }

    static string GenerateBundleWin(BuildTarget target, string suffix, string filePath)
    {
        //Generate thumbnail
        GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(filePath);

        if (obj == null)
            return string.Empty;

        GenerateAttachmentPartsThumbnails.GenerateAssetsThumbnail(obj);

        string[] assets = new string[2];

        assets[0] = filePath;
        assets[1] = filePath + ".png";

        AssetBundleBuild[] buildMap = new AssetBundleBuild[1];

        string bundleName = Path.GetFileName(filePath) + "_" + suffix;

        buildMap[0].assetBundleName = bundleName;
        buildMap[0].assetNames = assets;

        BuildPipeline.BuildAssetBundles(BUNDLES_PATH, buildMap, BuildAssetBundleOptions.ChunkBasedCompression, target);

        return bundleName;
    }
}
