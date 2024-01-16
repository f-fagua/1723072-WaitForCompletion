using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Image Creator Data", menuName = "Unity Support/SpawnManagerScriptableObject", order = 1)]
public class ImageCreatorSettings : ScriptableObject
{
    public int Width;
    public int Height;

    public string OutputFolder;

    public string DefaultName;

    public int NumberOfImages;
}
