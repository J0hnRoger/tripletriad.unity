using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Manager<SoundManager>
{
    private AudioSource _audioSource;

    public enum Sound
    {
        ClicButton
    }

    private Dictionary<Sound, AudioClip> _sounds;

    protected override void Awake()
    {
        base.Awake();
        _sounds = new Dictionary<Sound, AudioClip>();
        foreach (Sound sound in Enum.GetValues(typeof(Sound)))
        {
            _sounds[sound] = Resources.Load<AudioClip>(sound.ToString());
        }
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(Sound sound)
    {
        _audioSource.PlayOneShot(_sounds[sound]);
    }
}
