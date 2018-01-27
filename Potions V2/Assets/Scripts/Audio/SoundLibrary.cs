using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundLibrary : MonoBehaviour
{
    [Tooltip("For actions that have more than 1 sound. For example, if you have three jump sounds, you make a jump GroupID 'Jump' and add all three sounds to the group")]
    public SoundGroup[] soundGroups;

    Dictionary<string, AudioClip[]> groupDictionary = new Dictionary<string, AudioClip[]>();

    public void Awake()
    {
        foreach (SoundGroup soundGroup in soundGroups)
        {
            groupDictionary.Add(soundGroup.groupID, soundGroup.group);
        }
    }

    public AudioClip GetClipFromName(string name)
    {
        if (groupDictionary.ContainsKey(name))
        {
            AudioClip[] sounds = groupDictionary[name];
            return sounds[Random.Range(0, sounds.Length)];
        }
        return null;
    }

    [System.Serializable]
    public class SoundGroup
    {
        public string groupID;
        public AudioClip[] group;
    }
}