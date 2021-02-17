using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using System; // nos permite usar Array.Find() en lugar de un foreach

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    // Start is called before the first frame update
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>(); // gameObject es el actual GameObject del tipo AudioManager
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name); // Find the sound in sounds array where sound's name is equal to name
        if (s == null)
        {
            Debug.LogWarning("Sound:" + name + " not found!");
            return;
        }
        s.source.Play(); // Método Play de AudioSource Component
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name); // Find the sound in sounds array where sound's name is equal to name
        if (s == null)
        {
            Debug.LogWarning("Sound:" + name + " not found!");
            return;
        }
        s.source.Stop(); // Método Play de AudioSource Component
    }
}
