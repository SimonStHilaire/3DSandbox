using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class GenerateAttachmentPartsThumbnails
{
    [MenuItem("Assets/Generate selected thumbnail")]
    static void GenerateAssetsThumbnails()
    {
        GameObject selectedObject = Selection.activeGameObject;

        GenerateAssetsThumbnail(selectedObject);
    }

    static public void GenerateAssetsThumbnail(GameObject go)
    {
        string thumbnailFilename = GenerateObjectAssetsThumbnail(go);

        if (!string.IsNullOrEmpty(thumbnailFilename))
        {
            SetThumbnailAs2DSprite(thumbnailFilename);
        }
    }

    static string GenerateObjectAssetsThumbnail(GameObject obj)
    {
        AssetDatabase.StartAssetEditing();

        string thumbnailPath = AssetDatabase.GetAssetPath(obj.GetInstanceID());

        if (obj == null)
            return string.Empty;

        Texture2D thumbnail = RuntimePreviewGenerator.GenerateModelPreview(obj.transform);

        byte[] _bytes = thumbnail.EncodeToPNG();

        string fullPath = thumbnailPath + ".png";

        System.IO.File.WriteAllBytes(fullPath, _bytes);

        AssetDatabase.ImportAsset(fullPath);

        SetThumbnailAs2DSprite(fullPath);

        AssetDatabase.StopAssetEditing();

        return fullPath;
    }

    static public void SetThumbnailAs2DSprite(string path)
    {
        AssetDatabase.StartAssetEditing();

        TextureImporter importer = (TextureImporter)TextureImporter.GetAtPath(path);

        if (importer)
        {
            importer.mipmapEnabled = false;
            importer.textureType = TextureImporterType.Sprite;
            importer.SaveAndReimport();
        }

        AssetDatabase.StopAssetEditing();
    }
}
