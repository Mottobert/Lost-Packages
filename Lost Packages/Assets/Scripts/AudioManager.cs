﻿using UnityEngine.Audio;
using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        s.source.Play();
    }

    public void PlayRandomOfKind(string kind, int length)
    {
        Sound[] elements = new Sound[length];

        for(int i = 0; i < elements.Length; i++)
        {
            if (i < 10)
            {
                elements[i] = Array.Find(sounds, sound => sound.name == kind + "0" + (i + 1));
            }
            else
            {
                elements[i] = Array.Find(sounds, sound => sound.name == kind + (i + 1));
            }
        }
        int r = Random.Range(0, elements.Length);

        Debug.Log(elements[r]);
        elements[r].source.Play();
    }
}
