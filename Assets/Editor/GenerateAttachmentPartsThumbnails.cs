using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class GenerateAttachmentPartsThumbnails
{
    [MenuItem("Assets/Generate thumbnail")]
    static void GenerateAssetsThumbnails()
    {
        AssetDatabase.StartAssetEditing();

        GameObject selectedObject = Selection.activeGameObject;

        string thumbnailPath = AssetDatabase.GetAssetPath(selectedObject.GetInstanceID());

        if (selectedObject == null)
            return;

        Texture2D thumbnail = RuntimePreviewGenerator.GenerateModelPreview(selectedObject.transform);

        byte[] _bytes = thumbnail.EncodeToPNG();

        string fullPath = thumbnailPath + selectedObject.name + ".png";

        System.IO.File.WriteAllBytes(fullPath, _bytes);

        AssetDatabase.ImportAsset(fullPath);

        AssetDatabase.StopAssetEditing();

        SetThumbnailAs2DSprite(fullPath);
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
