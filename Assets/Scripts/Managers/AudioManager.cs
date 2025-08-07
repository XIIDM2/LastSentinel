using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour, IGameManager
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;

    [SerializeField] private AudioClip _mainMenuMusic;
    [SerializeField] private AudioClip _hoverButtonSound;
    [SerializeField] private AudioClip _pressButtonSound;

    private void Awake()
    {
        if (_musicSource == null && _sfxSource == null)
        {
            Debug.LogError("Please assign audiosources in AudioManager");
        }
    }

    private void Start()
    {

        _sfxSource.playOnAwake = false;

        _musicSource.playOnAwake = true;
        _musicSource.loop = true;
        _musicSource.priority = 60;

        PlayMainMenuMusic();
    }

    public void PlayHoverSound()
    {
        _sfxSource.PlayOneShot(_hoverButtonSound);
    }

    public void PlayPressSound()
    {
        _sfxSource.PlayOneShot(_pressButtonSound);
    }

    public void PlayClip(AudioClip clip)
    {
        _sfxSource.PlayOneShot(clip);
    }

    public void PlayMainMenuMusic()
    {
        _musicSource.clip = _mainMenuMusic;
        _musicSource.Play();
    }

}
