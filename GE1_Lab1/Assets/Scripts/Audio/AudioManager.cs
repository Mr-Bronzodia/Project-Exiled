using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public void Play(string name, GameObject source)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        
        if(s == null)
        {
            Debug.LogError("Sound " + name + "  not found!");
            return;
        }

        AudioSource[] currentSounds = source.GetComponents<AudioSource>();

        foreach(AudioSource playingSound in currentSounds)
        {
            if(playingSound.clip.name == s.clip.name & currentSounds.Length != 0)
            {
                Debug.Log("E");
                return;
            }
        }

        AudioSource newSource = source.AddComponent<AudioSource>();

        Debug.Log("C");
        newSource.clip = s.clip;
        newSource.volume = s.volume;
        newSource.pitch = s.pich;
        newSource.playOnAwake = true;
        newSource.spatialBlend = s.specialBlend;

        newSource.Play();

        if (s.loop)
        {
            newSource.loop = s.loop;
        }
        else
        {
            Destroy(newSource, s.clip.length);
        }
    }

    public void Play(string name, GameObject source, float volume)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogError("Sound " + name + "  not found!");
            return;
        }

        AudioSource[] currentSounds = source.GetComponents<AudioSource>();

        foreach (AudioSource playingSound in currentSounds)
        {
            if (playingSound.clip.name == s.clip.name & currentSounds.Length != 0)
            {
                Debug.Log("E");
                return;
            }
        }

        AudioSource newSource = source.AddComponent<AudioSource>();

        Debug.Log("C");
        newSource.clip = s.clip;
        newSource.volume = volume;
        newSource.pitch = s.pich;
        newSource.playOnAwake = true;
        newSource.spatialBlend = s.specialBlend;

        newSource.Play();

        if (s.loop)
        {
            newSource.loop = s.loop;
        }
        else
        {
            Destroy(newSource, s.clip.length);
        }
    }

    public void Stop(string name, GameObject source)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogError("Sound " + name + "  not found!");
            return;
        }

        AudioSource[] currentSounds = source.GetComponents<AudioSource>();

        foreach (AudioSource playingSound in currentSounds)
        {
            if (playingSound.clip.name == s.clip.name & currentSounds.Length != 0)
            {
                Destroy(playingSound);
            }
        }
    }
}

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    [Range(0f, 2f)]
    public float pich;
    
    [Range(0f, 1f)]
    public float specialBlend;

    public bool loop;

    [HideInInspector]
    public GameObject soundSource;
}



