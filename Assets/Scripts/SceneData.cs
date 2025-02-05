using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scene Manager", fileName = "New Scene Manager")]
public class SceneData : ScriptableObject
{
    [SerializeField] private string loadName;

    public static string LoadNameKey = "LoadName";

    public void SetLoadName(string loadName)
    {
        PlayerPrefs.SetString(LoadNameKey, loadName);
        this.loadName = loadName;
    }
    public string LoadName()
    {
        return loadName;
    }
}
