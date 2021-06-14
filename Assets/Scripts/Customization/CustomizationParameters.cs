using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "CustomizationParameters", menuName = "Params/CustomizationParameters", order = 1)]
public class CustomizationParameters : ScriptableObject
{
    public string AssetBundlesRemotePath;
}
