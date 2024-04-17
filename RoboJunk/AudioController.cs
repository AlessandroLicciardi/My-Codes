using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    AudioSource[] Sources;

    private void Awake()
    {
        Sources = GetComponents<AudioSource>();
    }

    public void GeneralSound(AudioClip _clip)
    {
        for(int i = 0; i < Sources.Length; i++)
        {
            if(!Sources[i].isPlaying)
            {
                Sources[i].clip = _clip;
                Sources[i].volume = 0.3f;
                Sources[i].pitch = 1 + Random.Range(-0.2f, 0.3f);
                Sources[i].Play();
                break;
            }
        }
    }

    public void PlayClip(AudioClip _clip)
    {
        for(int i = 0; i < Sources.Length; i++)
        {
            if(!Sources[i].isPlaying)
            {
                Sources[i].clip = _clip;
                Sources[i].volume = 0.8f;
                Sources[i].Play();
                break;
            }
        }
    }
}
