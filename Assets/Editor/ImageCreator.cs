using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.WSA;
using Application = UnityEngine.Application;

public class ImageCreator : MonoBehaviour
{
    private static string s_SettingsPath = "Assets/Data/Image Creator Data.asset";
    private static string s_ImageContainerPath = "Assets/Data/Image Container.asset";
    
    [MenuItem("Unity Support/Create Images")]
    public static void CreateImages()
    {
        var imageCreatorSettings = AssetDatabase.LoadAssetAtPath<ImageCreatorSettings>(s_SettingsPath);
        
        for (int i = 0; i < imageCreatorSettings.NumberOfImages; i++)
        {
            CreateImageAsset(imageCreatorSettings.OutputFolder, imageCreatorSettings.DefaultName, i, imageCreatorSettings.Width, imageCreatorSettings.Height);
        }
    }

    private static void CreateImageAsset(string folder, string imageName, int index, int width, int height)
    {
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGBA32, false);

        // loop over the texture's pixels
        for (int y = 0; y < tex.height; y++)
        {
            for (int x = 0; x < tex.width; x++)
            {
                Color color = new Color(Random.value, Random.value, Random.value);
                tex.SetPixel(x, y, color);
            }
        }
        tex.Apply(); // Apply the changes we made to the Texture2D.

        // Encode texture into PNG
        byte[] bytes = tex.EncodeToPNG();
        Object.DestroyImmediate(tex);

        // Write to a file in the project folder
        if (!Directory.Exists(Path.Combine("Assets", folder)))
            Directory.CreateDirectory(Path.Combine("Assets", folder));
        
        var filePath = Path.Combine(Application.dataPath, folder, $"{imageName}_{(index + 1)}.png");
        File.WriteAllBytes(filePath, bytes);
        AssetDatabase.Refresh();

        // Load asset and set TextureImporter settings
        var assetPath = Path.Combine("Assets", folder, $"{imageName}_{(index + 1)}.png");
        TextureImporter ti = TextureImporter.GetAtPath(assetPath) as TextureImporter;
        ti.textureType = TextureImporterType.Default;
 
        // Reimport asset to apply settings
        AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);

    }

    [MenuItem("Unity Support/Add Images to SO")]
    public static void SetImages()
    {
        var imageCreatorSettings = AssetDatabase.LoadAssetAtPath<ImageCreatorSettings>(s_SettingsPath);
        var imageContainer = AssetDatabase.LoadAssetAtPath<ImageContainer>(s_ImageContainerPath);

        var assetGUIDs = AssetDatabase.FindAssets("t:Texture2D", new string[] { Path.Combine("Assets", imageCreatorSettings.OutputFolder) });

        var textures = new Texture2D[assetGUIDs.Length];

        for (int i = 0; i < assetGUIDs.Length; i++)
        {
            var assetPath = AssetDatabase.GUIDToAssetPath(assetGUIDs[i]);
            var texture = AssetDatabase.LoadAssetAtPath<Texture2D>(assetPath);
            Debug.Log($"{assetPath} - <{texture}>");
            textures[i] = texture;
        }

        imageContainer.images = textures;
    }
}