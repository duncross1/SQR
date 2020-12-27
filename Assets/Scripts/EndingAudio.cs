using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingAudio : MonoBehaviour
{
    public AudioClip PostPickUpClip;


    void Awake()
    {
        Destroy(GameObject.Find("GameMusicSource"));
    }

    public void changeClip()
    {
        GetComponent<AudioSource>().clip = PostPickUpClip;
        GetComponent<AudioSource>().Play();
    }
}
