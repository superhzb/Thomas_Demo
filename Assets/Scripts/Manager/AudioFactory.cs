using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFactory 
{

    protected Dictionary<string, AudioClip> factoryDict = new Dictionary<string, AudioClip>();
    protected string loadPath;

    public AudioFactory()
    {
        loadPath = "AudioClips/";
    }

    public AudioClip GetClip(string name)
    {
        AudioClip clip = null;

        string itemPath = loadPath + name;
        if (factoryDict.ContainsKey(name))
        {
            clip = factoryDict[name];
        }
        else
        {
            clip = Resources.Load<AudioClip>(itemPath);
            factoryDict.Add(name, clip);
        }

        //Safety Verification
        if (clip == null)
        {
            Debug.LogWarning(clip + " does not exist in path: " + itemPath);
        }

        return clip;
    }

}
