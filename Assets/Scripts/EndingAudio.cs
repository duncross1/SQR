using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class EndingAudio : MonoBehaviour
{

    public Sound[] sounds;

    [HideInInspector]
    public bool isPlaying;

    void Awake()
    {


        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = true;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }

        
        
    }

    void Start()
    {
        //Destroy AudioManager
        if (FindObjectOfType<AudioManager>() != null)
        {
            Debug.Log("found audio manager");
            Destroy(FindObjectOfType<AudioManager>().gameObject);
        }

        PlaySong("PrePickup");
    }

    void Update()
    {
    }

    public void PlaySong(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
        isPlaying = true;
    }

    public void StopSong(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
        isPlaying = false;
    }



}
