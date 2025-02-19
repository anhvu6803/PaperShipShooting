using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scene Manager", fileName = "New Scene Manager")]
public class SceneData : ScriptableObject
{
    public static string LoadNameKey = "LoadName";

    public void SetLoadName(string loadName)
    {
        PlayerPrefs.SetString(LoadNameKey, loadName);
    }
    public string LoadName()
    {
        return PlayerPrefs.GetString(LoadNameKey);
    }
}
