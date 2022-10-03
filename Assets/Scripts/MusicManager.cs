using System;
using System.Collections.Generic;
using UnityEngine;

public enum Music
{
    Irelia_Login_Music
}

public class MusicManager : Manager<MusicManager>
{
    private Dictionary<Music, AudioClip> _musics;
    private AudioSource _audioSource;
    public BoolVariable IsMute; 

    protected override void Awake()
    {
        base.Awake();
        _musics = new Dictionary<Music, AudioClip>();
        foreach (Music music in Enum.GetValues(typeof(SoundManager.Sound)))
        {
            _musics[music] = Resources.Load<AudioClip>(music.ToString());
        }
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic(Music music)
    {
        if (IsMute.Value)
            return;
        _audioSource.clip = _musics[music];
        _audioSource.Play();
    }
}

