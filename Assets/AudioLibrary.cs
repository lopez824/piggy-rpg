using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLibrary : MonoBehaviour
{
    public List<AudioClip> audioClips;

    // Static Dictionary to reference audioclips
    public static Dictionary<string, AudioClip> library = new Dictionary<string, AudioClip>();
    
    private void Awake()
    {
        foreach (AudioClip clip in audioClips)
        {
            library.Add(clip.name, clip);
            //Debug.Log(clip.name);
        }
            
    }
}
