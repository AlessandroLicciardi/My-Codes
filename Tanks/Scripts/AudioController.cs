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
   public void PlayClip(AudioClip _clip, float volume)
   {
       for(int i = 0; i < Sources.Length ; i++)
       {
           if(!Sources[i].isPlaying)
           {
               Sources[i].clip = _clip;
               Sources[i].volume = volume;
               Sources[i].Play();
               break;
           }
       }
   }
}
