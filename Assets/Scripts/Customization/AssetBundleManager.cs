using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class AssetBundleManager : SceneSingleton<AssetBundleManager>
{
    public string RemotePath;

    [Serializable]
    public class Bundle
    {
        public string name;
        public int version;
    }

    [Serializable]
    public class BundleManifest
    {
        public List<Bundle> bundles = new List<Bundle>();
    }

    BundleManifest Manifest = null;

    const string MANIFEST_FILENAME = "manifest.json";

    Dictionary<string, AssetBundle> LoadedBundles = new Dictionary<string, AssetBundle>();

    public string[] GetAttachmentNames()
    {
        string[] attachments = new string[Manifest.bundles.Count];

        if (Manifest != null)
        {
            for (int i = 0; i < Manifest.bundles.Count; ++i)
            {
                attachments[i] = Manifest.bundles[i].name;
            }
        }

        return attachments;
    }

    public bool BundleExists(string name)
    {
        return File.Exists(Application.persistentDataPath + Path.AltDirectorySeparatorChar + name.ToLower());
    }

    public AttachmentPart LoadAttachment(string name)
    {
        AssetBundle bundle = AssetBundle.LoadFromFile(Application.persistentDataPath + Path.AltDirectorySeparatorChar + name.ToLower());
        LoadedBundles[name] = bundle;

        return Instantiate(bundle.LoadAsset<AttachmentPart>(name));
    }

    public void Initialize()
    {
        if (File.Exists(GetManifestFilePath()))
        {
            Manifest = JsonUtility.FromJson<BundleManifest>(File.ReadAllText(GetManifestFilePath()));
        }

        StartCoroutine(DownloadFile(MANIFEST_FILENAME, DownloadAttachments));
    }

    public void DownloadAttachments()
    {
        string filePath = GetManifestFilePath();

        if (File.Exists(filePath))
        {
            BundleManifest manifest = JsonUtility.FromJson<BundleManifest>(File.ReadAllText(filePath));

            foreach(Bundle bundle in manifest.bundles)
            {
                Bundle localBundle = Manifest != null ? Manifest.bundles.Where(i => i.name == bundle.name).First() : null;

                if(localBundle == null || bundle.version > localBundle.version)
                {
                    StartCoroutine(DownloadFile(bundle.name));
                }
            }
        }
    }
    string GetManifestFilePath()
    {
        return Application.persistentDataPath + Path.AltDirectorySeparatorChar + MANIFEST_FILENAME;
    }

    IEnumerator DownloadFile(string name, Action callback = null)
    {
        string uri = RemotePath + name;
          
        UnityWebRequest request = UnityWebRequest.Get(uri);

        yield return request.SendWebRequest();

        while (!request.downloadHandler.isDone)
        {
            Debug.Log(request.downloadProgress.ToString());
            yield return new WaitForFixedUpdate();
        }

        if (request.result == UnityWebRequest.Result.Success)
        {
            byte[] data = request.downloadHandler.data;

            File.WriteAllBytes(Application.persistentDataPath + Path.AltDirectorySeparatorChar + name, request.downloadHandler.data);
        }

        callback?.Invoke();
    }
}
