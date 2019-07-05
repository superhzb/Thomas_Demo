using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is the object class for json file.
/// </summary>
[System.Serializable]
public class UIPanelPathList
{
    public List<UIPanelPath> panelPathList;

    public struct UIPanelPath
    {
        public string name;
        public string path;
    }

    public override string ToString()
    {
        Debug.Log(panelPathList.Count);
        string totalString = "";
        foreach (var t in panelPathList)
        {
            totalString += t.ToString() + "\n";
        }
        return totalString;
    }
}