using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MusicManager : MonoBehaviour
{
    public AudioClip[] Clip;
    public AudioSource Music;

    static MusicManager One;

    private void Start()
    {
        One = this;
    }

    public void PlayMusic(int _music)
    {
        if (Clip.Length > _music)
        {
            Debug.Log("Play Music");
            Music.clip = Clip[_music];
            Music.Play();
        }
    }
}
