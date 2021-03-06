using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


[RequireComponent(typeof(AudioSource))]
public class ObjectSounds : MonoBehaviour
{
    List<AudioSource> currentAudioSources = new List<AudioSource>();
    bool didPlay = false;

    private void Start()
    {
        currentAudioSources.Add(gameObject.GetComponent<AudioSource>());
    }

    public void Play(AudioClip clip, AudioMixerGroup group)
    {
            foreach (AudioSource source in currentAudioSources)
        {
            if (source.isPlaying)
                continue;

            didPlay = true;
            source.PlayOneShot(clip);
            source.outputAudioMixerGroup = group;
            break;
        }

            if (!didPlay)
        {
            AudioSource temp = gameObject.AddComponent<AudioSource>();
            currentAudioSources.Add(temp);
            temp.PlayOneShot(clip);
            temp.outputAudioMixerGroup = group;
        }

        didPlay = false;
        
    }
}
