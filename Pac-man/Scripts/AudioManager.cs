using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource[] audioSources;

    private void Awake() 
    {
        audioSources = GetComponents<AudioSource>();
    }
    public void PlayClip(AudioClip _clip)
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            if(!audioSources[i].isPlaying)
            {
                audioSources[i].clip = _clip;
                audioSources[i].Play();
                break;
             }
        }
    }
}
