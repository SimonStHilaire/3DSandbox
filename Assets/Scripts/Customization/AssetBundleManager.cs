using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class AssetBundleManager : SceneSingleton<AssetBundleManager>
{
    public delegate void DownloadComplete();
    public DownloadComplete OnDownloadComplete;
    [SerializeField]
    private string RemotePath;

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
    int DownloadCount = 0;

    const string ATTACHMENTS_MANIFEST_FILENAME = "manifest.json";
    const string CARS_MANIFEST_FILENAME = "carsmanifest.json";

    Dictionary<string, AssetBundle> LoadedBundles = new Dictionary<string, AssetBundle>();

    public string[] GetCarNames()
    {
        BundleManifest carsManifest = JsonUtility.FromJson<BundleManifest>(File.ReadAllText(GetCarsManifestFilePath()));

        if (carsManifest == null)
            return new string[0];

        string[] cars = new string[carsManifest.bundles.Count];

        for (int i = 0; i < carsManifest.bundles.Count; ++i)
        {
            cars[i] = carsManifest.bundles[i].name;
        }

        return cars;
    }

    public string[] GetAttachmentNames()
    {
        if (Manifest == null)
        {
            Manifest = JsonUtility.FromJson<BundleManifest>(File.ReadAllText(GetManifestFilePath()));

            if (Manifest == null)
                return new string[0];
        }

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

    public List<VehiculeManager> GetAllVehicules()
    {
        List<VehiculeManager> cars = new List<VehiculeManager>();

        string[] carNames = GetCarNames();

        foreach (string name in carNames)
        {
            if (File.Exists(Application.persistentDataPath + Path.AltDirectorySeparatorChar + name.ToLower()))
                cars.Add(LoadCar(name));
        }

        return cars;
    }

    public List<AttachmentPart> GetAllParts()
    {
        List<AttachmentPart> parts = new List<AttachmentPart>();

        string[] attachmentNames = GetAttachmentNames();

        foreach (string name in attachmentNames)
        {
            if (File.Exists(Application.persistentDataPath + Path.AltDirectorySeparatorChar + name.ToLower()))
                parts.Add(LoadAttachment(name));
        }

        return parts;
    }

    public AttachmentPart LoadAttachment(string name)
    {
        AssetBundle bundle = AssetBundle.LoadFromFile(Application.persistentDataPath + Path.AltDirectorySeparatorChar + name.ToLower());
        LoadedBundles[name] = bundle;

        string[]assets = bundle.GetAllAssetNames();

        GameObject obj = null;

        if (assets.Length > 0)
        {
            obj = Instantiate(bundle.LoadAsset<GameObject>(assets[0]));
        }


        if (obj)
        {
            if (assets.Length > 1)
                obj.GetComponent<AttachmentPart>().Thumbnail = Instantiate(bundle.LoadAsset<Sprite>(assets[1]));

            return obj.GetComponent<AttachmentPart>();
        }
            

        return null;
    }

    public VehiculeManager LoadCar(string name)
    {
        AssetBundle bundle = AssetBundle.LoadFromFile(Application.persistentDataPath + Path.AltDirectorySeparatorChar + name.ToLower());
        LoadedBundles[name] = bundle;

        string[] assets = bundle.GetAllAssetNames();

        GameObject obj = null;

        if (assets.Length > 0)
        {
            obj = Instantiate(bundle.LoadAsset<GameObject>(assets[0]));
        }


        if (obj)
        {
            if (assets.Length > 1)
                obj.GetComponent<VehiculeManager>().Thumbnail = Instantiate(bundle.LoadAsset<Sprite>(assets[1]));

            return obj.GetComponent<VehiculeManager>();
        }


        return null;
    }

    public void Initialize()
    {
        if (string.IsNullOrEmpty(RemotePath))
        {
            CustomizationParameters parameters = Resources.Load<CustomizationParameters>("CustomizationParameters");

            if (parameters != null)
                RemotePath = parameters.AssetBundlesRemotePath;
        }

        if (File.Exists(GetManifestFilePath()))
        {
            Manifest = JsonUtility.FromJson<BundleManifest>(File.ReadAllText(GetManifestFilePath()));
        }

        StartCoroutine(DownloadFile(ATTACHMENTS_MANIFEST_FILENAME, DownloadAttachments));
    }

    public void DownloadAttachments()
    {
        string filePath = GetManifestFilePath();

        List<string> downloads = new List<string>();

        if (File.Exists(filePath))
        {
            BundleManifest manifest = JsonUtility.FromJson<BundleManifest>(File.ReadAllText(filePath));

            foreach(Bundle bundle in manifest.bundles)
            {
                Bundle localBundle = Manifest != null ? Manifest.bundles.Where(i => i.name == bundle.name).First() : null;

                if(localBundle == null || bundle.version > localBundle.version)
                {
                    downloads.Add(bundle.name);
                }
            }
        }

        if (downloads.Count > 0)
        {
            DownloadCount = downloads.Count;

            foreach (string name in downloads)
                StartCoroutine(DownloadFile(name, OnFileDownloadComplete));
        }
        else
        {
            OnDownloadComplete();
        }
    }

    void OnFileDownloadComplete()
    {
        --DownloadCount;

        if (DownloadCount <= 0 && OnDownloadComplete != null)
            OnDownloadComplete();
    }

    string GetCarsManifestFilePath()
    {
        return Application.persistentDataPath + Path.AltDirectorySeparatorChar + CARS_MANIFEST_FILENAME;
    }

    string GetManifestFilePath()
    {
        return Application.persistentDataPath + Path.AltDirectorySeparatorChar + ATTACHMENTS_MANIFEST_FILENAME;
    }

    IEnumerator DownloadFile(string name, Action callback = null)
    {
        string uri = RemotePath + name;
          
        UnityWebRequest request = UnityWebRequest.Get(uri);

        yield return request.SendWebRequest();

        while (!request.downloadHandler.isDone &&
           (request.result == UnityWebRequest.Result.InProgress && request.result != UnityWebRequest.Result.ConnectionError && request.result != UnityWebRequest.Result.DataProcessingError))

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
